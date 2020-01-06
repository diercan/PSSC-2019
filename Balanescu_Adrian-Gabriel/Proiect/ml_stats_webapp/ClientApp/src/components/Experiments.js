import React, { Component } from 'react';
import axios from "axios";
import Experiment from "./Experiment";

const EXP_URL = 'https://localhost:5001/api/experiments';

export class Experiments extends Component {
  
  constructor(props) {
    super(props);
    this.state = {
      isFetching: false,
      experiments: []
    };
  }

  render() {
    return (
      <Experiment/>
    );
  }

  componentDidMount() {
    this.fetchUsers();
  }

  async fetchUsersAsync() {
    try {
      this.setState({...this.state, isFetching: true});
      const response = await axios.get(EXP_URL);
      this.setState({experiments: response.data, isFetching: false});
    } catch (e) {
      console.log(e);
      this.setState({...this.state, isFetching: false});
    }
  };
  
  fetchUsers = this.fetchUsersAsync
}