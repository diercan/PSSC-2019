import React, { useState } from "react";
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
import InputLabel from "@material-ui/core/InputLabel";
import AddAlert from "@material-ui/icons/AddAlert";
// core components
import Grid from '@material-ui/core/Grid';
import GridItem from "components/Grid/GridItem.js";
import GridContainer from "components/Grid/GridContainer.js";
import CustomInput from "components/CustomInput/CustomInput.js";
import Button from "components/CustomButtons/Button.js";
import Card from "components/Card/Card.js";
import CardHeader from "components/Card/CardHeader.js";
import CardAvatar from "components/Card/CardAvatar.js";
import CardBody from "components/Card/CardBody.js";
import CardFooter from "components/Card/CardFooter.js";
import 'date-fns';
import DateFnsUtils from '@date-io/date-fns';
import SnackbarContent from "components/Snackbar/SnackbarContent.js";
import Snackbar from "components/Snackbar/Snackbar.js";
import {
    MuiPickersUtilsProvider,
    KeyboardTimePicker,
    KeyboardDatePicker,
} from '@material-ui/pickers';
import TextField from '@material-ui/core/TextField';



const styles = {
    cardCategoryWhite: {
        color: "rgba(255,255,255,.62)",
        margin: "0",
        fontSize: "14px",
        marginTop: "0",
        marginBottom: "0"
    },
    cardTitleWhite: {
        color: "#FFFFFF",
        marginTop: "0px",
        minHeight: "auto",
        fontWeight: "300",
        fontFamily: "'Roboto', 'Helvetica', 'Arial', sans-serif",
        marginBottom: "3px",
        textDecoration: "none"
    },
};

const useStyles = makeStyles(styles);

export default function InsertEvents() {
    const classes = useStyles();
    const [selectedDateStart, setSelectedDateStart] = React.useState(new Date());
    const [selectedDateEnd, setSelectedDateEnd] = React.useState(new Date());
    const [tl, setTL] = React.useState(false);
    const handleDateChangeStart = date => {
        setSelectedDateStart(date);
    };
    const handleDateChangeEnd = date => {
        setSelectedDateEnd(date);
    };
    function saveEventHandler() {
        if (!tl) {
            setTL(true);
            setTimeout(function() {
              setTL(false);
            }, 1200);
          }
        const title = document.getElementById("title").value;
        const description = document.getElementById("description").value;
        const id = localStorage.getItem("id");
        const payload = {
            "title": title,
            "description":description,
            "startDate": selectedDateStart,
            "endDate": selectedDateEnd
        }
        fetch("https://my-events-pssc.herokuapp.com/api/event/insertEvent/"+id, {
            method: 'post',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
          })
          .then(response => response.json())
          .then((response) => {
          })
          .catch(err => console.log(err))
    }

    // React.useEffect(() => {
    //     console.log("rerendering>!");
    // }, [questions]);

    return (
        <div>
            <GridContainer>
                <GridItem xs={12} sm={12} md={1}>
                </GridItem>
                <GridItem xs={12} sm={12} md={10}>
                    <Card>
                        <CardHeader color="primary">
                            <h4 className={classes.cardTitleWhite}>Add new event on your personal calendar</h4>
                        </CardHeader>
                        <CardBody>
                            <GridContainer>
                                <GridItem xs={12} sm={12} md={6}>
                                    <CustomInput
                                        labelText="Title"
                                        id="title"
                                        formControlProps={{
                                            fullWidth: true
                                        }}
                                        inputProps={{
                                            disabled: false
                                        }}
                                    />
                                </GridItem>
                            </GridContainer>
                            <GridContainer>
                                <GridItem xs={12} sm={12} md={12}>

                                    <CustomInput
                                        labelText="Short description for your event"
                                        id="description"
                                        formControlProps={{
                                            fullWidth: true
                                        }}
                                        inputProps={{
                                            multiline: true,
                                            rows: 5
                                        }}
                                    />
                                </GridItem>
                            </GridContainer>
                            <GridContainer>
                                <GridItem xs={12} sm={12} md={12}>
                                <InputLabel style={{ color: "#AAAAAA" }}>Start Date</InputLabel>
                                    <MuiPickersUtilsProvider utils={DateFnsUtils}>
                                        <Grid container justify="space-around">
                                            <KeyboardDatePicker
                                                margin="normal"
                                                id="date-picker-dialog-start"
                                                label="Date picker dialog"
                                                format="MM/dd/yyyy"
                                                value={selectedDateStart}
                                                onChange={handleDateChangeStart}
                                                KeyboardButtonProps={{
                                                    'aria-label': 'change date',
                                                }}
                                            />
                                            <KeyboardTimePicker
                                                margin="normal"
                                                id="time-picker-start"
                                                label="Time picker"
                                                value={selectedDateStart}
                                                onChange={handleDateChangeStart}
                                                KeyboardButtonProps={{
                                                    'aria-label': 'change time',
                                                }}
                                            />
                                        </Grid>
                                    </MuiPickersUtilsProvider>
                                </GridItem>
                                <GridItem xs={12} sm={12} md={12}>
                                <InputLabel style={{ color: "#AAAAAA" }}>End Date</InputLabel>
                                    <MuiPickersUtilsProvider utils={DateFnsUtils}>
                                        <Grid container justify="space-around">
                                            <KeyboardDatePicker
                                                margin="normal"
                                                id="date-picker-dialog-end"
                                                label="Date picker dialog"
                                                format="MM/dd/yyyy"
                                                value={selectedDateEnd}
                                                onChange={handleDateChangeEnd}
                                                KeyboardButtonProps={{
                                                    'aria-label': 'change date',
                                                }}
                                            />
                                            <KeyboardTimePicker
                                                margin="normal"
                                                id="time-picker-end"
                                                label="Time picker"
                                                value={selectedDateEnd}
                                                onChange={handleDateChangeEnd}
                                                KeyboardButtonProps={{
                                                    'aria-label': 'change time',
                                                }}
                                            />
                                        </Grid>
                                    </MuiPickersUtilsProvider>
                                </GridItem>
                            </GridContainer>


                            {/* <GridContainer>
                                <GridItem xs={12} sm={12} md={4}>
                                    <CustomInput
                                        labelText="City"
                                        id="city"
                                        formControlProps={{
                                            fullWidth: true
                                        }}
                                    />
                                </GridItem>
                                <GridItem xs={12} sm={12} md={4}>
                                    <CustomInput
                                        labelText="Country"
                                        id="country"
                                        formControlProps={{
                                            fullWidth: true
                                        }}
                                    />
                                </GridItem>
                                <GridItem xs={12} sm={12} md={4}>
                                    <CustomInput
                                        labelText="Postal Code"
                                        id="postal-code"
                                        formControlProps={{
                                            fullWidth: true
                                        }}
                                    />
                                </GridItem>
                            </GridContainer>
                            <GridContainer>
                                <GridItem xs={12} sm={12} md={12}>
                                    <InputLabel style={{ color: "#AAAAAA" }}>About me</InputLabel>
                                    <CustomInput
                                        labelText="Lamborghini Mercy, Your chick she so thirsty, I'm in that two seat Lambo."
                                        id="about-me"
                                        formControlProps={{
                                            fullWidth: true
                                        }}
                                        inputProps={{
                                            multiline: true,
                                            rows: 5
                                        }}
                                    />
                                </GridItem>
                            </GridContainer> */}

                        </CardBody>

                        <CardFooter>

                            <Button color="primary" onClick={saveEventHandler}>Add Event</Button>
                        </CardFooter>
                    </Card>
                </GridItem>
                <Snackbar
                  place="tr"
                  color="info"
                  icon={AddAlert}
                  message="New event added in calendar!"
                  open={tl}
                  closeNotification={() => setTL(false)}
                  close
                />
            </GridContainer>
        </div>
    );
}
