import React from 'react'
import {Button, Card, CardBody, CardFooter, CardHeader, CardText, CardTitle} from "reactstrap";
import {Link} from "react-router-dom";
import {Redirect} from "react-router";

const Experiment = (props) => {
  return (
    <div>{props.data.map(exp => (
      <Card key={exp.id} className={"p-2 m-2"}>
        <CardHeader>{exp.experimentName}</CardHeader>
        <CardBody>
          <CardTitle>{exp.experimentDesc}</CardTitle>
          <CardText>Model Architecture</CardText>
          <Link to={{pathname: '/experiment/' + exp.id, state: {expId: exp.id}}}>Detailed View</Link>
        </CardBody>
        <CardFooter>Added @ {exp.createdAt}</CardFooter>
      </Card>
    ))}
      <p>{props.isFetching ? 'Fetching data...' : ''}</p>
      <hr/>
    </div>
  )
};
export default Experiment