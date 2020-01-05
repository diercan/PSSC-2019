/*eslint-disable*/
import React, { useState } from "react";
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
import Hidden from "@material-ui/core/Hidden";
// core components
import GridItem from "components/Grid/GridItem.js";
import GridContainer from "components/Grid/GridContainer.js";
import Card from "components/Card/Card.js";
import CardHeader from "components/Card/CardHeader.js";
import CardBody from "components/Card/CardBody.js";
import { Calendar, momentLocalizer } from 'react-big-calendar'
import moment from 'moment'
import styles from "assets/jss/material-dashboard-react/views/iconsStyle.js";
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Button from '@material-ui/core/Button';

const useStyles = makeStyles(styles);
const localizer = momentLocalizer(moment)

Date.prototype.addHours= function(h){
  this.setHours(this.getHours()+h);
  return this;
}

function useFetch(url) {
  const [loading, setLoading] = React.useState(true)
  const [data, setData] = React.useState(null)
  const [error, setError] = React.useState(null)

  React.useEffect(() => {
    setLoading(true)
    const id = localStorage.getItem("id");



    fetch(url + id)
      .then((res) => res.json())
      .then((data) => {
        console.log("data_information", data);
        let newCalendarEvents = [];
        let popupEvents = [];
        let id = 0;
        data.events.forEach(event => {
          newCalendarEvents.push({
            "id": id,
            "title": event.title,
            "start": new Date(event.startDate),
            "end": new Date(event.endDate)
          });
          popupEvents.push({
            "id": id,
            "title": event.title,
            "description": event.description,
            "startDate": event.startDate,
            "endDate": event.endDate
          });
          id++;
          console.log("calendarEvents", newCalendarEvents);
        })
        let e ={
          "events": newCalendarEvents,
          "popup": popupEvents
        }
        console.log("final data", e);
        setData(e)
        setError(null)
        setLoading(false)
      })
      .catch((e) => {
        let ev ={
          "events": [],
          "popup": []
        }
        console.log("final data", e);
        setData(ev)
        console.warn(e.message)
        setError(null)
        setLoading(false)
      })
  }, [url])

  return { loading, data, error }
}



export default function Icons() {
  const classes = useStyles();

  const { loading, data: requestData, error } = useFetch(
    `https://my-events-pssc.herokuapp.com/api/event/getAllEvents/`
  )
  const [open, setOpen] = React.useState(false);
  const [event, setEvent] = React.useState({});

  const handleClickOpen = (event) => {
    const found = requestData.popup.find(element => element.title === event.title);
    setEvent(found);

    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };


  if (loading === true) {
    return <p>Loading</p>
  }
  return (

    <GridContainer>
      <GridItem xs={12} sm={12} md={12}>
        <Card >
          <CardHeader plain color="primary">
            <h4 className={classes.cardTitleWhite}>Your personal Calendar</h4>
          </CardHeader>
          <CardBody>
            <div>
              
              <Calendar
                localizer={localizer}
                events={requestData.events}
                startAccessor="start"
                endAccessor="end"
                style={{ height: 500 }}
                onSelectEvent={event => handleClickOpen(event)}
              />
            </div>

          </CardBody>
        </Card>
      </GridItem>
      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">{event.title}</DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            <GridContainer>
            
            <GridItem xs={12} sm={12} md={12}>
              {event.description}
            </GridItem>
            <GridItem xs={12} sm={12} md={12}>
            </GridItem>
            <GridItem xs={12} sm={12} md={6}>
            {moment(event.startDate).format('MMMM Do YYYY, h:mm:ss a')}
            </GridItem>
            <GridItem xs={12} sm={12} md={6}>
            {moment(event.endDate).format('MMMM Do YYYY, h:mm:ss a')}
            </GridItem>
            </GridContainer>
            
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose} color="primary">
            Disagree
          </Button>
          <Button onClick={handleClose} color="primary" autoFocus>
            Agree
          </Button>
        </DialogActions>
      </Dialog>
    </GridContainer>
  );
}
