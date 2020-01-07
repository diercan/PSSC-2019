import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import './custom.css'
import {Experiments} from "./components/Experiments";
import {Comparer} from "./components/Comparer";

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Experiments} />
        <Route path='/experiments' component={Experiments} />
        <Route path='/comparer' component={Comparer} />
      </Layout>
    );
  }
}
