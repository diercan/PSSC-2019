import React, { useState } from "react";
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
// core components
import GridItem from "components/Grid/GridItem.js";
import GridContainer from "components/Grid/GridContainer.js";
import Table from "components/Table/Table.js";
import Card from "components/Card/Card.js";
import CardHeader from "components/Card/CardHeader.js";
import CardBody from "components/Card/CardBody.js";
import TablePoll from "components/TablePoll/TablePoll.js"
import VoteCardPresentation from "components/VoteCard/VoteCardPresentation.js"
const axios = require('axios');

function createData(name, desc, startDate, endDate, pollId) {
  // const density = population / size;
  return { name, desc, startDate, endDate, pollId };
}
const styles = {
  cardCategoryWhite: {
    "&,& a,& a:hover,& a:focus": {
      color: "rgba(255,255,255,.62)",
      margin: "0",
      fontSize: "14px",
      marginTop: "0",
      marginBottom: "0"
    },
    "& a,& a:hover,& a:focus": {
      color: "#FFFFFF"
    }
  },
  cardTitleWhite: {
    color: "#FFFFFF",
    marginTop: "0px",
    minHeight: "auto",
    fontWeight: "300",
    fontFamily: "'Roboto', 'Helvetica', 'Arial', sans-serif",
    marginBottom: "3px",
    textDecoration: "none",
    "& small": {
      color: "#777",
      fontSize: "65%",
      fontWeight: "400",
      lineHeight: "1"
    }
  }
};

const useStyles = makeStyles(styles);

function useFetch(url) {
  const [loading, setLoading] = React.useState(true)
  const [data, setData] = React.useState(null)
  const [error, setError] = React.useState(null)

  React.useEffect(() => {
    setLoading(true)
    const user = localStorage.getItem("user", user);
    const cnp = user.cnp;


    axios.get(url, {
      headers: {
        'Access-Control-Allow-Origin': '*',
        'x-user-cnp': cnp
      },
      withCredentials: true
    })
      .then(function (response) {
        console.log("POLL LIST", response.data);
        const rows = [];
        response.data.forEach((poll, index) => {
          let cPoll = poll[0];
          console.log(cPoll.name, cPoll.description, cPoll.startDate, cPoll.endDate);
          rows.push(createData(cPoll.name, cPoll.description, cPoll.startDate, cPoll.endDate, cPoll.id));
        });
        setData(rows)
        setError(null)
        setLoading(false)
      })

      .catch(function (error) {
        console.warn(error.message)
        setError('Error fetching data. Try again.')
        setLoading(false)
      })
    // fetch(url)
    //   .then((res) => res.json())
    //   .then((data) => {
    //     setData(data)
    //     setError(null)
    //     setLoading(false)
    //   })
    //   .catch((e) => {
    //     console.warn(e.message)
    //     setError('Error fetching data. Try again.')
    //     setLoading(false)
    //   })
  }, [url])

  return { loading, data, error }
}

export default function TableList() {
  const classes = useStyles();
  const { loading, data: rows, error } = useFetch(
    `http://localhost:4010/api/v1.0/platform/election`
  )
  const [cardState, setCardState] = useState(false);
  const [poll, setPoll] = useState({});

  function handleRowReq(id) {
    const url = "http://localhost:4010/api/v1.0/platform/election/" + id;
    const user = localStorage.getItem("user", user);
    const cnp = user.cnp;
    axios.get(url, {
      headers: {
        'Access-Control-Allow-Origin': '*',
        'x-user-cnp': cnp
      },
      withCredentials: true
    })
      .then(function (response) {
        console.log("POLL REQ", response.data);

        setPoll(response.data[0][0])
        setCardState(true);
      })

      .catch(function (error) {
        console.warn(error.message)
      })
  }

  if (loading === true) {
    return <p>Loading</p>
  }


  return (
    <GridContainer>
      <GridItem xs={12} sm={12} md={12}>

        {!cardState ?
          <Card>
            <CardHeader color="primary">
              <h4 className={classes.cardTitleWhite}>Poll List</h4>
            </CardHeader>
            <CardBody>
              <TablePoll rows={rows} handleRowReq={handleRowReq}></TablePoll>
            </CardBody>
          </Card> :
            <VoteCardPresentation poll = {poll}></VoteCardPresentation>
        }

      </GridItem>
    </GridContainer>
  );
}
