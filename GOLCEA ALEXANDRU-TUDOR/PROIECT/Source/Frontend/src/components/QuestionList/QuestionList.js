import React, { useEffect } from 'react';
import GridContainer from "components/Grid/GridContainer.js";
import QuestionCard from "components/QuestionCard/QuestionCard";
import Button from "components/CustomButtons/Button.js";
import GridItem from "components/Grid/GridItem.js";
export default function QuestionList(props) {

    React.useEffect(() => {
        console.log("UPDATED QUESTIONS", props.questions);
    }, [props.questions]);


    function handleChange() {
        console.log("change of option")
    }
    return (
        <div>
            {props.questions.map((question, index) => (
                <GridContainer key={question.id}>
                    {console.log("question", question)}

                    <QuestionCard
                        id = {question.id}
                        saveQuestionHandler = {props.saveQuestionHandler}
                        editFunctionHandler = {props.editFunctionHandler}
                        handleChange = {handleChange}
                        editState = {question.editState}
                    // user={user}
                    // handleUserUpdate={props.handleUserUpdate}
                    // handleUserRemove={props.handleUserRemove}
                    />
                </GridContainer>
            ))}

        </div>

    )
}
