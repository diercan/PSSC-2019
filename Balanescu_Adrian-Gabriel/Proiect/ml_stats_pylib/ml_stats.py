import requests
import json

from rabbitmq_service import QService

API_URL = "https://localhost:5001/api"
RMQ_HOST = "localhost"


def obj_dict(obj):
    return obj.__dict__


class Experiment:
    def __init__(self, expName, expDesc, modelArchitecture, trainingParams, userName):
        self.expId = None
        self.expName = expName
        self.expDesc = expDesc
        self.modelArchitecture = modelArchitecture
        self.trainingParams = trainingParams
        self.userName = userName
        self.userId = None
        self.plots = []

    def cfgAddPlot(self, plot_title, xAxisTitle, yAxisTitle):
        plot = Plot(plot_title, xAxisTitle, yAxisTitle)
        self.plots.append(plot)

    def addToDb(self):
        pload = {"userName": self.userName}
        r_user = requests.post(API_URL + "/users", json=pload, verify=False)
        r_dictionary = r_user.json()
        self.userId = r_dictionary['id']

        for plt in self.plots:
            pltload = {
                "title": plt.title,
                "xAxisTitle": plt.xAxisTitle,
                "yAxisTitle": plt.yAxisTitle,
            }
            r_plt = requests.post(API_URL + "/experiments/plots", json=pltload, verify=False)
            r_plt_dict = r_plt.json()
            plt.pltId = r_plt_dict['id']

        load = {"experimentName": self.expName,
                "experimentDesc": self.expDesc,
                "createdById": self.userId,
                "tfModelArchitecture": json.loads(self.modelArchitecture),
                "trainingParams": self.trainingParams,
                "listOfPlots": json.dumps(self.plots, default=obj_dict)}

        r_exp = requests.post(API_URL + "/experiments", json=load, verify=False)
        r_exp_dictionary = r_exp.json()
        self.expId = r_exp_dictionary['id']


class PlotPoint:
    def __init__(self, pltId, x, y):
        self.plotId = pltId
        self.xValue = x
        self.yValue = y


class Plot:
    def __init__(self, title, xAxisTitle, yAxisTitle):
        self.pltId = None
        self.title = title
        self.xAxisTitle = xAxisTitle
        self.yAxisTitle = yAxisTitle


class MlStats:

    def __init__(self, experiment: Experiment):
        self.experiment = experiment
        self.qservice = QService(RMQ_HOST, 'experiments')

    def sendLivePlotData(self, title, x, y):
        for plt in self.experiment.plots:
            if plt.title == title:
                point = PlotPoint(plt.pltId, x, y)
                self.qservice.send_live("exps_stream", json.dumps(point, default=obj_dict))

# if __name__ == "__main__":
#     ml = MlStats("sdkf", "sdf", "sdkjf", "sdkf", "Adrian")
#     print()
