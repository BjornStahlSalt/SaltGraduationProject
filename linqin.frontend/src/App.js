import { useEffect, useState } from 'react';
import './App.css';
import Home from './Components/Home/Home.js';
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
    setCurrentLevel(null);
  }, [levels]);

  const handleTitleClick = (level) => {
    setCurrentLevel(level);
  };

  return (
    <div className='Page'>
      <nav className='Nav'>
        <div className='Nav__Links'>
          <a href="NotYet"> <img className="Nav__Img" src="https://cdn-icons-png.flaticon.com/512/25/25694.png"></img></a>
          <a href="NotYet"> <img className="Nav__Img" src="https://upload.wikimedia.org/wikipedia/commons/thumb/9/98/OOjs_UI_icon_bookmark.svg/1200px-OOjs_UI_icon_bookmark.svg.png"></img></a>
        </div>
        <button className='Nav__LevelButton' onClick={() => setCurrentLevel(null)}>Home</button>
        <h2 className='Nav__Title'>Levels</h2>
        <div className='Nav__LevelList'>
          {
            levels.map((l) =>
              <button className='Nav__LevelButton' type="submit" key={l.id} onClick={() => handleTitleClick(l)}>{l.title}</button>)
          }
        </div>
      </nav>
      <div className='Page__Content'>
        {currentLevel === null ? <Home levels={levels} setCurrentLevel={setCurrentLevel} /> : <Level level={currentLevel} />}
      </div>
    </div>
  );
}

export default App;
