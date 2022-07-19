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

        listOfShapes: [
          { Shape: 'Circle', Color: 'Red', PriorityValue: 2 },
          { Shape: 'Triangle', Color: 'Green', PriorityValue: 3 },
          { Shape: 'Square', Color: 'Blue', PriorityValue: 1 }
        ],
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
      <Collection levels={ levels } />
      <input type="text" />
      <button>Submit</button>
    </div>
  );
}

export default App;
