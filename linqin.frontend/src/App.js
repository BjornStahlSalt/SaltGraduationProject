import { useEffect, useState } from 'react';
import './App.css';
import Collection from './Components/Collection/Collection.js';
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
    // const requestOptions = {
    //   method: 'POST',
    //   headers: {
    //     'Content-Type': 'application/json',
    //   },
    //   body: JSON.stringify({
    //     listOfShapes: levels[0].startCollection,
    //     Query: 'test.OrderBy(s => s.PriorityValue);'
    //   }),
    // };

    // fetch('https://localhost:7003/api/Inputs', requestOptions)
    //   .then(response => response.json());

    setCurrentLevel(levels[0]);
    console.log(levels);
  }, [levels]);

  const handleTitleClick = (level) => {
    //setCurrentLevel({title: level.title, id: level.id, description: level.description, linqMethod: level.linqMethod, startCollection: level.startCollection, expectedCollection: level.expectedCollection});
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
              <button className='Nav__LevelButton' type="submit" key={l.id} onClick={() => handleTitleClick(l)}>{l.title}</button>)
          }
        </div>
      </nav>
      <Level level={currentLevel} />
    </div>
  );
}

export default App;
