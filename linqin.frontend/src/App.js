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
  const handleNextClick = () => {
    if (levels.indexOf(currentLevel) < levels.length - 1) {
      setCurrentLevel(levels[levels.indexOf(currentLevel) + 1]);
    }
  };
  const handlePrevClick = () => {
    if (levels.indexOf(currentLevel) > 0) {
      setCurrentLevel(levels[levels.indexOf(currentLevel) - 1]);
    }
  };



  return (
    <div className='Page'>
      <Nav levels={orderedLevelsArray} handleTitleClick={handleTitleClick} handleHomeClick={handleHomeClick} />
      <div className='Page__Content'>
        {currentLevel === null ? <Home levels={levels} setCurrentLevel={setCurrentLevel} /> : <Level level={currentLevel} handleNextClick={handleNextClick} handlePrevClick={handlePrevClick} />}
      </div>
    </div>
  );
}

export default App;
