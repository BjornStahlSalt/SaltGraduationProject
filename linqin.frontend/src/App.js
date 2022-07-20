import { useEffect, useState } from 'react';
import './App.css';
import Collection from './Components/Collection/Collection.js';

function App() {

  const [levels, setLevels] = useState([]);
  useEffect(() => {
    const requestOptions = {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({

        listOfShapes: levels.startCollection,
        Query: 'test.OrderBy(s => s.PriorityValue);'
      }),
    };
    fetch('https://localhost:7003/api/Inputs', requestOptions)
      .then(response => response.json());

    fetch('https://localhost:7186/api/Levels')
      .then(response => response.json())
      .then(data => setLevels(data));
  }, []);
  useEffect(() => {
    console.log(levels);
  }, [levels]);


  return (
    <div className="App">
      <header></header>
      <div>
        <h2>Levels</h2>
        <div>
          {levels.map((l) => <h3 key={l.id}>{l.title}</h3>)}
        </div>
      </div>

      <div>
        <h3>Title</h3>
        <p>Prompt</p>
        <div>
          <Collection shapes={levels[0].startCollection} />
          <Collection shapes={levels[0].expectedCollection} />
        </div>
      </div>
    </div>
  );
}

export default App;
