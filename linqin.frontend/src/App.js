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


  return (
    <div className="App">
      <header></header>
      <div>
        <h2>Levels</h2>
        <div>
          { levels.map((l) => <h3 key={ l.id }>{ l.title }</h3>) }
        </div>
      </div>
      <Level level={ currentLevel } />
    </div>
  );
}

export default App;
