import React, { useState } from "react";
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
import InputLabel from "@material-ui/core/InputLabel";
// core components
import GridItem from "components/Grid/GridItem.js";
import GridContainer from "components/Grid/GridContainer.js";
import CustomInput from "components/CustomInput/CustomInput.js";
import Button from "components/CustomButtons/Button.js";
import Card from "components/Card/Card.js";
import CardHeader from "components/Card/CardHeader.js";
import CardAvatar from "components/Card/CardAvatar.js";
import CardBody from "components/Card/CardBody.js";
import CardFooter from "components/Card/CardFooter.js";

import avatar from "assets/img/faces/marc.jpg";
import QuestionCard from "components/QuestionCard/QuestionCard";
import QuestionList from "components/QuestionList/QuestionList";
import axios from "axios";
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
    }
};

const useStyles = makeStyles(styles);

export default function InsertPoll() {
    const [questions, setQuestions] = useState([{ "id": 1, "name": "", "options": [], "editState":true }]);
    const classes = useStyles();

    function addQuestionHandler() {
        // let newQuestions = questions;
        let newId = questions[questions.length - 1].id + 1;
        // questions.push({"id":newId,"name":"", "options": []});
        setQuestions([...questions, { "id": newId, "name": "", "options": [], "editState":true }]);
        console.log("questions", questions);
    }

    function saveQuestionHandler(id, question) {
        console.log("saveQuestionHandler:::", id, question)
        let newQuestionsState = questions.map((stateQ, index)=>{
            if (index === id-1) {
                stateQ.name = question.name;
                stateQ.options = question.options;
                stateQ.editState = false;
            }
            return stateQ;
        })
        console.log(newQuestionsState);
        setQuestions(newQuestionsState);
        // props.questions[id-1].editState = false;
    }

    function editFunctionHandler(id) {
        console.log("editFunctionHandler:::", id)
        let newQuestionsState = questions.map((stateQ, index)=>{
            if (index === id-1) {
                stateQ.editState = true;
            }
            return stateQ;
        })
        console.log(newQuestionsState);
        setQuestions(newQuestionsState);
        // props.questions[id-1].editState = false;
    }

    function savePoll() {
        let url = 'http://localhost:4010/api/v1.0/platform/election';
        let saveQuestionState = questions.map((stateQ, index)=>{
            let saveQuestion = {
                "name" : stateQ.name,
                "options": stateQ.options
            }
            return saveQuestion;
        })
        axios.post(url, {
            "name": document.getElementById("poll-name").value,
            "description": document.getElementById("poll-description").value,
            "startDate": document.getElementById("poll-start-date").value,
            "endDate": document.getElementById("poll-end-date").value,
            "questions": saveQuestionState
          }, {
            headers: {
                'x-user-cnp': 'asd',
            },
            withCredentials : true
          })
          .then(function (response) {

            alert("Poll Saved!");
          })
          .catch(function (error) {
            console.log(error);
            alert("Faild to save poll!");
          });
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
                            <h4 className={classes.cardTitleWhite}>Add poll</h4>
                            <p className={classes.cardCategoryWhite}>Create new poll.</p>
                        </CardHeader>
                        <CardBody>
                            <GridContainer>
                                <GridItem xs={12} sm={12} md={6}>
                                    <CustomInput
                                        labelText="Poll Name:"
                                        id="poll-name"
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
                                    <InputLabel style={{ color: "#AAAAAA" }}>Description</InputLabel>
                                    <CustomInput
                                        labelText="Short poll description"
                                        id="poll-description"
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
                                <GridItem xs={12} sm={12} md={6}>
                                    <CustomInput
                                        labelText="Start Date"
                                        id="poll-start-date"
                                        formControlProps={{
                                            fullWidth: true
                                        }}
                                    />
                                </GridItem>
                                <GridItem xs={12} sm={12} md={6}>
                                    <CustomInput
                                        labelText="End Date"
                                        id="poll-end-date"
                                        formControlProps={{
                                            fullWidth: true
                                        }}
                                    />
                                </GridItem>
                            </GridContainer>
                            <QuestionList questions={questions} saveQuestionHandler = {saveQuestionHandler}
                                        editFunctionHandler = {editFunctionHandler}
                                        ></QuestionList>
                            <GridContainer>
                                <GridItem xs={12} sm={12} md={9}></GridItem>
                                <GridItem xs={12} sm={12} md={2}><Button color="primary" onClick={addQuestionHandler}>Add Question</Button></GridItem>

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

                            <Button color="primary" onClick= {savePoll}>Save Poll</Button>
                        </CardFooter>
                    </Card>
                </GridItem>
            </GridContainer>
        </div>
    );
}
