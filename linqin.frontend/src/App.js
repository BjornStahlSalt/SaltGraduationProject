import { useEffect } from 'react';
import './App.css';

function App() {

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
      .then(response => response.json())
      .then(data => console.log(data));
  }, []);

  return (
    <div className="App">
      <header className="App-header">
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
