import tensorflow as tf

from ml_stats import MlStats, Experiment


class LossAndErrorPrintingCallback(tf.keras.callbacks.Callback):
    def __init__(self, mlstats: MlStats):
        self.ml = mlstats

    def on_train_batch_end(self, batch, logs=None):
        ml.sendLivePlotData("Loss/Batch", x=batch, y=logs['loss'].item())
        #print('For batch {}, loss is {:7.2f}.'.format(batch, logs['loss']))


    def on_test_batch_end(self, batch, logs=None):
        print('For batch {}, loss is {:7.2f}.'.format(batch, logs['loss']))


mnist = tf.keras.datasets.mnist

(x_train, y_train), (x_test, y_test) = mnist.load_data()
x_train, x_test = x_train / 255.0, x_test / 255.0

model = tf.keras.models.Sequential([
  tf.keras.layers.Flatten(input_shape=(28, 28)),
  tf.keras.layers.Dense(128, activation='relu'),
  tf.keras.layers.Dropout(0.2),
  tf.keras.layers.Dense(10, activation='softmax')
])

model.compile(optimizer='adam',
              loss='sparse_categorical_crossentropy',
              metrics=['accuracy'])

experiment = Experiment(
    expName="SignalR TEST 2",
    expDesc="DNN with fully connected layers",
    modelArchitecture=model.to_json(),
    trainingParams={"batch_size": 32},
    userName="AdrianB"
    )
experiment.cfgAddPlot("Loss/Batch", xAxisTitle="Batch num", yAxisTitle="Loss Value")
experiment.addToDb()

ml = MlStats(experiment=experiment)

model.fit(x_train, y_train, epochs=5, callbacks=[LossAndErrorPrintingCallback(mlstats=ml)])

model.evaluate(x_test,  y_test, verbose=2)