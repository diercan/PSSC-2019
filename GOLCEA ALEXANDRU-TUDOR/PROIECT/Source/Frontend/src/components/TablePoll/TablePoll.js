import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
// import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TablePagination from '@material-ui/core/TablePagination';
import TableRow from '@material-ui/core/TableRow';
import { Button } from '@material-ui/core';
const axios = require('axios');

function createData(name, description, startDate, endDate) {
  // const density = population / size;
  return { name, description, startDate, endDate };
}
const columns = [
  { id: 'name', label: 'Name', minWidth: 170 },
  { id: 'desc', label: 'Description', minWidth: 100 },
  {
    id: 'startDate',
    label: 'Start Date',
    minWidth: 170,
  },
  {
    id: 'endDate',
    label: 'End Date',
    minWidth: 170,
  },
  {
    id: 'more',
    label: 'More Information',
    minWidth: 170,
    align: 'right',
    format: value => value.toFixed(2),
  },
];








const useStyles = makeStyles({
  root: {
    width: '100%',
  },
  container: {
    maxHeight: 440,
  },
});

async function handleRowReq()  {
  const user = localStorage.getItem("user", user);
  const cnp = user.cnp;
  const url = "http://localhost:4010/api/v1.0/platform/election";

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
      response.data.forEach((poll, index)=>{
        let cPoll = poll[0];
        console.log(cPoll.name, cPoll.description, cPoll.startDate, cPoll.endDate);
        rows.push(createData(cPoll.name, cPoll.description, cPoll.startDate, cPoll.endDate));
      });
      // UserProfile.setAuth(response.data[0]);
      return rows;
    })
    
    .catch(function (error) {
      console.log(error);
    })
    .finally(function () {
      // always executed
    });
}

export default function TablePoll(props) {
  const classes = useStyles();
  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(10);
  const rows = props.rows;
  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };



  const handleChangeRowsPerPage = event => {
    setRowsPerPage(+event.target.value);
    setPage(0);
  };

  function handleClickButton(id) {
    console.log("CURRENT ID", id);
    props.handleRowReq(id);
  }
  return (
    <Paper className={classes.root}>

      <Table stickyHeader aria-label="sticky table">
        <TableHead>
          <TableRow>
            {columns.map(column => (
              <TableCell
                key={column.id}
                align={column.align}
                style={{ minWidth: column.minWidth }}
              >
                {column.label}
              </TableCell>
            ))}
          </TableRow>
        </TableHead>
        <TableBody>
          {rows.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map(row => {
            return (
              <TableRow hover role="checkbox" tabIndex={-1} key={row.code}>
                {columns.map(column => {
                  const value = row[column.id];
                  return (
                    <TableCell key={column.id} align={column.align}>
                      {column.id === "more" ?
                        <Button color="primary" onClick={() => handleClickButton(row.pollId)}  >Register for Vote</Button> :
                        column.format && typeof value === 'number' ? column.format(value) : value}
                    </TableCell>
                  );
                })}
              </TableRow>
            );
          })}
        </TableBody>
      </Table>

      <TablePagination
        rowsPerPageOptions={[10, 25, 100]}
        component="div"
        count={rows.length}
        rowsPerPage={rowsPerPage}
        page={page}
        onChangePage={handleChangePage}
        onChangeRowsPerPage={handleChangeRowsPerPage}
      />
    </Paper>
  );
}