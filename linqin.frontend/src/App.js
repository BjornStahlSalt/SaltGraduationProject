import { useEffect, useState } from 'react';
import './App.css';
import Level from './Components/Level/Level.js';

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
      <header></header>
      <nav className='Nav'>
        <h2 className='Nav__Title'>Levels</h2>
        <div className='Nav__LevelList'>
          {
            levels.map((l) =>
              <button className='Nav__LevelButton' type="submit" key={ l.id } onClick={ () => handleTitleClick(l) }>{ l.title }</button>)
          }
        </div>
      </nav>
      <Level level={ currentLevel } />
    </div>
  );
}

export default App;
