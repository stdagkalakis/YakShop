import React from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import './App.css';
import Landing from './Components/Layout/Landing'
import Logo from './Components/Layout/Logo';
import { Container } from 'semantic-ui-react'
import Shepherd from './Components/Pages/Shepherd';
import Customer from './Components/Pages/Customer';


function App() {
  return (
    <Router>
      <div className="App">

        <Container>
          <Logo />
          <Switch>
            <Route exact path='/' render={Landing} />
            <Route exact path='/shepherd' render={(props) => { return <Shepherd {...props} /> }} />
            <Route exact path='/customer' render={(props) => { return <Customer {...props} /> }} />
          </Switch>
        </Container>
      </div>
    </Router>
  );
}

export default App;
