import './App.css';
//import react from 'react';
import {
  BrowserRouter as Router,
  Routes as Switch,
  Route,
  Link
} from "react-router-dom";

import Signin from './Signin';
import Signup from "./Signup";
function App() {
  return (
    <Router>
      <div>
        <nav>
          <ul>
            <li>
              <Link to="/" exact>Home</Link>{/*activeClassName="active"*/}
            </li>
            <li>
              <Link to="/about">About</Link>
            </li>
            <li>
              <Link to="/signin">Signin</Link>
            </li>
            <li>
              <Link to="/signup">Signup</Link>
            </li>
          </ul>
        </nav>

        {/* A <Switch> looks through its children <Route>s and
            renders the first one that matches the current URL. */}
        <Switch>
          <Route path="/about" element={<About/>}/>
          <Route path="/signin" element={<Signin/>}/>
          <Route path="/signup" element={<Signup/>}/>
          <Route path="/" exect element={<Home/>}/>
          <Route path="*" element={<Home/>}/>
          
        </Switch>
      </div>
    </Router>
  );
}

function Home() {
  return <h2>Home</h2>;
}

function About() {
  return <h2>About</h2>;
}

export default App;
