import React, { useState } from "react";
// material-ui components
import { makeStyles } from "@material-ui/core/styles";
// core components
import Card from "components/Card/Card.js";
import CardBody from "components/Card/CardBody.js";
import CardHeader from "components/Card/CardHeader.js";
import Button from "components/CustomButtons/Button.js";
import CustomInput from "components/CustomInput/CustomInput.js";
import { cardTitle } from "assets/jss/material-kit-react.js";
import GridContainer from "components/Grid/GridContainer.js";
import GridItem from "components/Grid/GridItem.js";
const styles = {
    cardTitle,
};

const useStyles = makeStyles(styles);

export default function QuestionCard(props) {
    const [options, setOptions] = useState([{ "id": 1, "name": "" }]);
    const classes = useStyles();



    function addOptionHandler() {
        let newId = options[options.length - 1].id + 1;
        setOptions([...options, { "id": newId, "name": "" }]);
        console.log("option", options);
    }

    function saveQuestionHandler() {
        let question = { "name": document.getElementById("question").value, "options": [] }
        let allOptions = [];
        options.map((option, index) => {
            allOptions.push({ "name": document.getElementById("option" + option.id).value });
        })
        question.options = allOptions;
        props.saveQuestionHandler(props.id, question)
    }

    function editFunctionHandler() {
        props.editFunctionHandler(props.id);
    }
    return (
        <Card style={{ width: "60rem" }}>
            <CardHeader color="warning">
                Question
            </CardHeader>
            <CardBody>
                <h4 className={classes.cardTitle}>Type your question:</h4>
                <CustomInput
                    labelText="Question..."
                    id="question"
                    formControlProps={{
                        fullWidth: true
                    }}
                    inputProps={{
                        disabled: !props.editState
                    }}
                />
                <p>
                    Add your options
                </p>
                {options.map((option, index) => (
                    <CustomInput key={option.id}

                        labelText="Option..."
                        id={"option" + option.id}
                        formControlProps={{
                            fullWidth: true
                        }}
                        inputProps={{
                            disabled: !props.editState
                        }}

                    />
                ))}
                {props.editState ?
                    <GridContainer>
                        <Button color="primary" onClick={addOptionHandler}>Add Option</Button>
                        <Button color="rose" onClick={saveQuestionHandler}>Save Question</Button>
                    </GridContainer>
                    :
                    <Button color="warning" onClick={editFunctionHandler}>Edit Question</Button>}

            </CardBody>
        </Card>
    );
}