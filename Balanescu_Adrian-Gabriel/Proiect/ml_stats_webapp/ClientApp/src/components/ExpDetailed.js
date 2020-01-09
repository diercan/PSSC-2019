import React, { Component } from 'react';
import axios from "axios";
import {HubConnectionBuilder, LogLevel} from "@microsoft/signalr"
const EXP_URL = 'https://localhost:5001/api/experiments/';

export class ExpDetailed extends Component {
  constructor(props) {
    super(props);
    this.state = {
      id: null,
      isFetching: false,
      experiment: [],
      hubConnection: null,
      expLiveData: [],
      listOfPlots: []
    };
  }
  
  render() {
    return (
      <div>
        <pre>{JSON.stringify(this.state.experiment, null, 2)}</pre>
        <hr/><br/>
        <pre>{JSON.stringify(this.state.expLiveData)}</pre>
      </div>
    );
  }
  
  componentDidMount () {
    const { expId } = this.props.location.state;
    this.setState({id:expId});
    this.fetchExperimentDetails(expId);
    const hubConnection = new HubConnectionBuilder()
      .withUrl("/expStream")
      .configureLogging(LogLevel.Information)
      .build();
    
    this.setState({hubConnection}, () => {
      this.state.hubConnection
        .start()
        .then(() => console.log("ConnectionStarted"))
        .catch(err => console.log('Error while establishing connection :('));
      this.state.hubConnection.on('onExpData', (expLiveData) => {
        const text = expLiveData;
        this.setState({expLiveData});
      })
    });
    
  }

  async fetchExperimentDetailsAsync(expId) {
    try {
      this.setState({...this.state, isFetching: true});
      const response = await axios.get(EXP_URL + expId).then(response => response.data).then((data) => {
        this.setState({experiment: data, isFetching: false});
        this.setState({listOfPlots: data.listOfPlots});
        
      });
      
    } catch (e) {
      console.log(e);
      this.setState({...this.state, isFetching: false});
    }
  }
  fetchExperimentDetails = this.fetchExperimentDetailsAsync;
}