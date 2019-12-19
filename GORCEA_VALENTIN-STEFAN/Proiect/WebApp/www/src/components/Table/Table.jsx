import React from 'react';
import PropTypes from 'prop-types';
import withStyles from "@material-ui/core/styles/withStyles";
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import Button from "components/CustomButtons/Button.jsx";
import People from "@material-ui/icons/People";
import Close from "@material-ui/icons/Close";
import Remove from "@material-ui/icons/Remove";
import Tooltip from "@material-ui/core/Tooltip";
import '../../assets/css/tableStyle.css';

const styles = theme => ({
  root: {
    width: '100%',
    marginTop: theme.spacing.unit * 3,
    overflowX: 'auto',
  },
  table: {
    minWidth: 700,
  },
  lightTooltip: {
    backgroundColor: theme.palette.common.white,
    color: 'rgba(0, 0, 0, 0.87)',
    boxShadow: theme.shadows[1],
    fontSize: 12,
    whiteSpace: 'pre-line'
  }
});


class SimpleTable extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      rows: props.tableData,
      currentUserName: props.currentUserName,
      userInfo: props.userInfo,
      showGIF: "none",
      accessedTraining: 0
    }
  }

  componentWillReceiveProps = ({ tableData, currentUserName, userInfo, showGIF, accessedTraining }) => {
    this.setState({
      rows: tableData,
      currentUserName: currentUserName,
      userInfo: userInfo,
      showGIF: showGIF,
      accessedTraining: accessedTraining
    })
  }

  render() {
    const { classes } = this.props;
    //let handler  =  this.props.handler;
    let handlerAssistToTraining = this.props.handlerAssistToTraining;
    let handlerLeaveTraining = this.props.handlerLeaveTraining;
    let handlerCancelTraining = this.props.handlerCancelTraining;
    return (
      <Paper className={classes.root}>
        {
          this.state.userInfo.isTrainer === 1 ? (<Table className={classes.table}>
            <TableHead>
              <TableRow>
                <TableCell style={{ paddingLeft: 6 }}>Action</TableCell>
                <TableCell style={{ paddingLeft: 4 }}>Topic</TableCell>
                <TableCell style={{ paddingLeft: 4 }}>Description</TableCell>
                <TableCell style={{ paddingLeft: 4 }} align="left">Location</TableCell>
                <TableCell style={{ paddingLeft: 4 }} align="left">Date (yyyy-mm-dd)</TableCell>
                <TableCell style={{ paddingLeft: 4 }} align="left">Trainer</TableCell>
                <TableCell style={{ paddingLeft: 4 }} align="left">Duration</TableCell>
                <TableCell style={{ paddingLeft: 4 }} align="left">Seats</TableCell>
                <TableCell style={{ paddingLeft: 4 }} align="left">Participate</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {this.props.tableData.map(row => (
                <TableRow key={row.id}>
                  <TableCell align="right">
                    {row.trainer == this.state.currentUserName ? <Button justIcon round color="danger" onClick={() => handlerCancelTraining(row.id)}>
                      <Close className={classes.icons} />
                    </Button> : <Button justIcon round>
                        <Remove className={classes.icons} />
                      </Button>
                    }
                  </TableCell>
                  <TableCell align="right">
                    {row.topic}
                  </TableCell>
                  <TableCell align="right">
                    {row.description}
                  </TableCell>
                  <TableCell align="right">{row.location}</TableCell>
                  <TableCell align="right">{row.scheduledOn.split(" ")[0]}</TableCell>
                  <TableCell align="right">{row.trainer}</TableCell>
                  <TableCell align="right">{row.durationHours + "h"}</TableCell>
                  <Tooltip
                    id="tooltip-left"
                    title={row.currentAttendeesList !== null ? row.currentAttendeesList.split(",").join("\n") : ""}
                    placement="left"
                    classes={{ tooltip: classes.lightTooltip }}
                  >
                    <TableCell align="right">{row.currentAttendees + "/" + row.maxAttendees}</TableCell>
                  </Tooltip>
                  <TableCell align="right">
                    {(() => {
                      if (this.state.showGIF !== "none" && this.state.accessedTraining === row.id) {
                        return <div className="spinner" id="loadingGif" style={{ display: this.state.showGIF }}>
                          <div className="bounce1"></div>
                          <div className="bounce2"></div>
                          <div className="bounce3"></div>
                        </div>
                      }
                      else {
                        if ((row.maxAttendees / row.currentAttendees) === 1 || (row.maxAttendees < row.currentAttendees)) {
                          if (row.isCurrentUserAnAttendee == "0") {
                            return <Button round>
                              No more seats
              </Button>
                          }
                          else {
                            return <Button round color="danger" onClick={() => handlerLeaveTraining(row.id)}>
                              <Close className={classes.icons} />Leave
              </Button>
                          }
                        }
                        else {
                          if (row.isCurrentUserAnAttendee == "0") {
                            return <Button round color="success" onClick={() => handlerAssistToTraining(row.id)}>
                              <People className={classes.icons} />Join
                </Button>
                          }
                          else {
                            return <Button round color="danger" onClick={() => handlerLeaveTraining(row.id)}>
                              <Close className={classes.icons} />Leave
              </Button>
                          }
                        }
                      }
                    })()}

                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>) :
            (<Table className={classes.table}>
              <TableHead>
                <TableRow>
                  <TableCell>Topic</TableCell>
                  <TableCell style={{ paddingLeft: 4 }} align="right">Location</TableCell>
                  <TableCell style={{ paddingLeft: 4 }} align="right">Date</TableCell>
                  <TableCell style={{ paddingLeft: 4 }} align="right">Trainer</TableCell>
                  <TableCell style={{ paddingLeft: 4 }} align="right">Duration</TableCell>
                  <TableCell style={{ paddingLeft: 4 }} align="right">Seats</TableCell>
                  <TableCell style={{ paddingLeft: 4 }} align="right">Participate</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {this.props.tableData.map(row => (
                  <TableRow key={row.id}>
                    <TableCell component="th" scope="row">
                      {row.topic}
                    </TableCell>
                    <TableCell align="right">{row.location}</TableCell>
                    <TableCell align="right">{row.scheduledOn}</TableCell>
                    <TableCell align="right">{row.trainer}</TableCell>
                    <TableCell align="right">{row.durationHours}.{row.durationMinutes === null || row.durationMinutes === '' ? 0 : row.durationMinutes}</TableCell>
                    <Tooltip
                      id="tooltip-left"
                      title={row.currentAttendeesList !== null ? row.currentAttendeesList.split(",").join("\n") : ""}
                      placement="left"
                      classes={{ tooltip: classes.tooltip }}
                    >
                      <TableCell align="right">{row.currentAttendees + "/" + row.maxAttendees}</TableCell>
                    </Tooltip>
                    <TableCell align="right">
                      {(() => {
                        if (this.state.showGIF !== "none" && this.state.accessedTraining === row.id) {
                          return <div className="spinner" id="loadingGif" style={{ display: this.state.showGIF }}>
                            <div className="bounce1"></div>
                            <div className="bounce2"></div>
                            <div className="bounce3"></div>
                          </div>
                        }
                        else {
                          if ((row.maxAttendees / row.currentAttendees) === 1 || (row.maxAttendees < row.currentAttendees)) {
                            if (row.isCurrentUserAnAttendee == "0") {
                              return <Button round>
                                No more seats
              </Button>
                            }
                            else {
                              return <Button round color="danger" onClick={() => handlerLeaveTraining(row.id)}>
                                <Close className={classes.icons} />Leave
              </Button>
                            }
                          }
                          else {
                            if (row.isCurrentUserAnAttendee == "0") {
                              return <Button round color="success" onClick={() => handlerAssistToTraining(row.id)}>
                                <People className={classes.icons} />Join
                </Button>
                            }
                            else {
                              return <Button round color="danger" onClick={() => handlerLeaveTraining(row.id)}>
                                <Close className={classes.icons} />Leave
              </Button>
                            }
                          }
                        }
                      })()}
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>)
        }
      </Paper>
    );
  }
}

SimpleTable.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(SimpleTable);