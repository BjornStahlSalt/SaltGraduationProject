import { useEffect, useState } from 'react';
import './App.css';
import Home from './Components/Home/Home.js';
import Level from './Components/Level/Level.js';
import Nav from './Components/Nav/Nav.js';
import './App.css';


function App() {

  const [levels, setLevels] = useState([]);
  const [currentLevel, setCurrentLevel] = useState(null);

  useEffect(() => {
    fetch('https://localhost:7186/api/Levels')
      .then(response => response.json())
      .then(data => setLevels(data));
  }, []);

  useEffect(() => {
    setCurrentLevel(null);
  }, [levels]);

  const handleTitleClick = (level) => {
    setCurrentLevel(level);
  };
  const handleHomeClick = () => {
    setCurrentLevel(null);
  };


  return (
    <div className='Page'>
      <Nav levels={ levels } handleTitleClick={ handleTitleClick } handleHomeClick={ handleHomeClick } />
      <div className='Page__Content'>
        { currentLevel === null ? <Home levels={ levels } setCurrentLevel={ setCurrentLevel } /> : <Level level={ currentLevel } /> }
      </div>
    </div>
  );
}

export default App;
