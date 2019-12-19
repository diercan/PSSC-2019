import React from "react";
import GridContainer from "components/Grid/GridContainer.jsx";
import withStyles from "@material-ui/core/styles/withStyles";
import SimpleTable from "components/Table/Table.jsx";
import Slide from "@material-ui/core/Slide";
import IconButton from "@material-ui/core/IconButton";
import Dialog from "@material-ui/core/Dialog";
import DialogTitle from "@material-ui/core/DialogTitle";
import DialogContent from "@material-ui/core/DialogContent";
import Button from "components/CustomButtons/Button.jsx";
import People from "@material-ui/icons/People";
import Check from "@material-ui/icons/Check";
import Close from "@material-ui/icons/Close";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import TextField from '@material-ui/core/TextField';
import basicsStyle from "assets/jss/material-kit-react/views/componentsSections/basicsStyle.jsx";
import "assets/css/tableStyle.css"

//for requests
import axios from 'axios';
axios.defaults.withCredentials = true;

function Transition(props) {
  return <Slide direction="down" {...props} />;
}

class SectionTable extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      tableData: [],
      trainingsNumber: 0,
      trainerTrainings: [],
      trainerMeetings: [],
      receivedTrainings: 0,
      userInfo: {},
      classicModal: false,
      showGIF: "none",
      accessedTraining: 0,
      checkAllTrainings: false,
      startDate: null,
      trainings: [],
      userName: localStorage.getItem("userName"),
      pass: localStorage.getItem("pass")
    };
  }
  handleClickOpen(modal) {
    var x = [];
    x[modal] = true;
    this.setState(x);
  }

  handleClose(modal) {
    var x = [];
    x[modal] = false;
    this.setState(x);
  }

  handlerGet = () => {
    axios.get(process.env.REACT_APP_URL + `/ts/training`, {
      headers: {
        "Access-Control-Allow-Origin": "*",
        "authorization": JSON.stringify({
          userName: this.state.userName,
          password: this.state.pass
        })
      },
      withCredentials: true
    })
      .then(list => {
        let total = list.data.trainings.reduce(function (prev, cur) {
          return prev + cur.isCurrentUserAnAttendee;
        }, 0);
        this.setState({
          tableData: list.data.trainings,
          trainingsNumber: total
        })
      });
  }

  handlerPost = (id) => {
    axios.post(process.env.REACT_APP_URL + `/ts/training/` + id, null, {
      headers: {
        "Access-Control-Allow-Origin": "*",
        "authorization": JSON.stringify({
          userName: this.state.userName,
          password: this.state.pass
        })
      },
      withCredentials: true
    })
      .then(list => {
        let total = list.data.reduce(function (prev, cur) {
          return prev + cur.isCurrentUserAnAttendee;
        }, 0);
        this.setState({
          tableData: list.data,
          trainingsNumber: total,
          showGIF: "none",
          accessedTraining: id
        })
      })
      .catch(error => {
        this.setState({
          showGIF: "none",
          accessedTraining: 0
        })
      });
  }

  handlerDelete = (id) => {
    axios.put(process.env.REACT_APP_URL + `/ts/training/leave/` + id, null, {
      headers: {
        "Access-Control-Allow-Origin": "*",
        "authorization": JSON.stringify({
          userName: this.state.userName,
          password: this.state.pass
        })
      },
      withCredentials: true
    })
      .then(list => {
        let total = list.data.reduce(function (prev, cur) {
          return prev + cur.isCurrentUserAnAttendee;
        }, 0);
        this.setState({
          tableData: list.data,
          trainingsNumber: total,
          showGIF: "none",
          accessedTraining: id
        })
      })
      .catch(error => {
        this.setState({
          showGIF: "none",
          accessedTraining: 0
        })
      });
  }

  handlerDeleteTraining = (id) => {
    axios.delete(process.env.REACT_APP_URL + `/ts/training/` + id, {
      headers: {
        "Access-Control-Allow-Origin": "*",
        "authorization": JSON.stringify({
          userName: this.state.userName,
          password: this.state.pass
        })
      },
      withCredentials: true
    })
      .then(list => {
        let total = list.data.reduce(function (prev, cur) {
          return prev + cur.isCurrentUserAnAttendee;
        }, 0);
        this.setState({
          tableData: list.data,
          trainingsNumber: total
        })
      });
  }

  handlerAssistToTraining = (id) => {
    this.setState({
      showGIF: "block",
      accessedTraining: id
    })
    this.handlerPost(id);
  }

  handleDateChange = (date) => {
    this.setState({
      startDate: date
    })
  }

  handlerLeaveTraining = (id) => {
    this.setState({
      showGIF: "block",
      accessedTraining: id
    })
    this.handlerDelete(id);
  }

  handlerCancelTraining = (id) => {
    this.handlerDeleteTraining(id);
  }

  componentDidMount = () => {
    this.handlerGet();
  }

  addNewTraining = () => {
    axios.post(process.env.REACT_APP_URL + `/ts/training/new`, {
      "topic": document.getElementById("topicField").value,
      "description": document.getElementById("descriptionField").value,
      "location": document.getElementById("locationField").value,
      "trainer": 1,
      "date": this.state.startDate,
      "duration": document.getElementById("durationField").value,
      "seats": document.getElementById("seatsField").value
    }, {
      headers: {
        "Access-Control-Allow-Origin": "*",
        "authorization": JSON.stringify({
          userName: this.state.userName,
          password: this.state.pass
        })
      },
      withCredentials: true
    })
      .then(list => {
        let total = list.data.reduce(function (prev, cur) {
          return prev + cur.isCurrentUserAnAttendee;
        }, 0);
        let resultTrainings = this.state.trainerTrainings.filter(obj => {
          return this.state.trainings.filter(training => obj.meetingId !== training.selectedTrainingId)
        })
        let resultMeetings = this.state.trainerMeetings.filter(obj => {
          return this.state.trainings.filter(training => obj.meetingId !== training.selectedTrainingId)
        })

        this.setState({
          tableData: list.data,
          trainingsNumber: total,
          trainerTrainings: resultTrainings,
          trainerMeetings: resultMeetings
        })
      });
  }
  componentWillReceiveProps = ({ userInfo }) => {
    this.setState({ userInfo: userInfo })
  }

  render() {
    const { classes } = this.props;
    return (
      <div className={classes.sections}>
        <div className={classes.container}>
          <div className={classes.title}>
            <h2>Available Trainings</h2>
            {
              this.state.userInfo.isTrainer === 1 ? (
                <Button id="meetingsTrainer" round color="primary" onClick={() => this.handleClickOpen("classicModal")}>
                  <People className={classes.icons} />Meetings
                </Button>)
                : null
            }
            {
              this.state.userInfo.isTrainer === 0 ? (<h6>You are going to {this.state.trainingsNumber} trainings!</h6>) : null
            }
            <Dialog
              classes={{
                root: classes.center,
                paper: classes.modal
              }}
              open={this.state.classicModal}
              TransitionComponent={Transition}
              keepMounted
              onClose={() => this.handleClose("classicModal")}
              aria-labelledby="classic-modal-slide-title"
              aria-describedby="classic-modal-slide-description"
            >
              <DialogTitle
                id="classic-modal-slide-title"
                disableTypography
                className={classes.modalHeader}
              >
                <IconButton
                  className={classes.modalCloseButton}
                  key="close"
                  aria-label="Close"
                  color="inherit"
                  onClick={() => this.handleClose("classicModal")}
                >
                  <Close className={classes.modalClose} />
                </IconButton>
                <h4 style={{ paddingRight: 320 }} className={classes.modalTitle}>Add new training</h4>
              </DialogTitle>
              <DialogContent
                id="classic-modal-slide-description"
                className={classes.modalBody}
              >
                <GridContainer key="firstContainer" justify="center">
                  <TextField
                    id="topicField"
                    style={{ margin: 8 }}
                    placeholder="Topic"
                    helperText="Insert topic here!"
                    fullWidth
                    margin="normal"
                    InputLabelProps={{
                      shrink: false,
                    }}
                  />
                  <br></br>
                  <TextField
                    id="descriptionField"
                    /* label="Label" */
                    style={{ margin: 8 }}
                    placeholder="Description"
                    helperText="Insert description here!"
                    fullWidth
                    margin="normal"
                    InputLabelProps={{
                      shrink: false,
                    }}
                  />
                  <br></br>
                  <TextField
                    id="locationField"
                    style={{ margin: 8 }}
                    placeholder="Location"
                    helperText="Insert location here!"
                    fullWidth
                    margin="normal"
                    InputLabelProps={{
                      shrink: false,
                    }}
                  />
                  <br></br>
                  <div style={{ margin: 8, width: "100%" }} >
                    <DatePicker
                      style={{ width: "100%" }}
                      selected={this.state.startDate}
                      minDate={new Date()}
                      placeholderText="Select training date!"
                      onChange={this.handleDateChange}
                    />
                  </div>
                  <br></br>
                  <TextField
                    id="durationField"
                    /* label="Label" */
                    style={{ margin: 8 }}
                    placeholder="Duration (hours)"
                    helperText="Insert duration here!"
                    fullWidth
                    type="number"
                    margin="normal"
                    InputLabelProps={{
                      shrink: false,
                    }}
                  />
                  <br></br>
                  <TextField
                    id="seatsField"
                    style={{ margin: 8 }}
                    placeholder="Available seats"
                    helperText="Insert number of available seats here!"
                    fullWidth
                    margin="normal"
                    type="number"
                    InputLabelProps={{
                      shrink: false,
                    }}
                  />
                </GridContainer>
                <GridContainer key="secondContainer" justify="center">
                  <Button style={{ marginBottom: 20 }} round color="success" onClick={() => this.addNewTraining()}>
                    <Check className={classes.icons} />Add
            </Button>
                </GridContainer>

              </DialogContent>
            </Dialog>
          </div>
          <GridContainer justify="center">
            <SimpleTable tableData={this.state.tableData} showGIF={this.state.showGIF} accessedTraining={this.state.accessedTraining} userInfo={this.state.userInfo} currentUserName={this.state.userInfo.officialName} handlerAssistToTraining={this.handlerAssistToTraining} handlerLeaveTraining={this.handlerLeaveTraining} handlerCancelTraining={this.handlerCancelTraining} />
          </GridContainer>
        </div>
      </div>
    );
  }
}

export default withStyles(basicsStyle)(SectionTable);
