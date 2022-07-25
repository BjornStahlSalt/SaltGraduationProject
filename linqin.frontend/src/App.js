import { useEffect, useState } from 'react';
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


    setCurrentLevel(levels[0]);
    console.log(levels);
  }, [levels]);

  const handleTitleClick = (level) => {
    setCurrentLevel(level);
  };


  return (
    <div className='Page'>
      <Nav levels={ levels } handleTitleClick={ handleTitleClick } />
      <div className='Page__Content'>
        <Level level={ currentLevel } />
      </div>
    </div>
  );
}

export default App;
