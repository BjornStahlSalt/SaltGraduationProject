import React, { useState, useEffect } from 'react';
import Collection from '../Collection/Collection.js';
import './Level.css';


function Level({ level }) {
  const [userInput, setUserInput] = useState("");
  const [compileError, setCompileError] = useState("");
  const [queryShapes, setQueryShapes] = useState([]);



  const submitAnswer = () => {
    if (level == null)
      return;

    const requestOptions = {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        listOfShapes: level.startCollection,
        Query: userInput
      }),
    };

    fetch('https://localhost:7003/api/Inputs', requestOptions)
      .then(response => response.json())
      .then(response => {
        if (response.errorMessage === null) {
          setQueryShapes(response.listOfShapes);
          checkAnswer(level.expectedCollection, response.listOfShapes);
        }
        else {
          setQueryShapes([]);
          setCompileError(response.errorMessage);
        }
      })
      .catch(error => console.log(error));
  };

  const checkAnswer = (expected, result) => {
    const expectedList = expected.map(s => ({ shape: s.shape, color: s.color, priorityValue: s.priorityValue }));
    if (JSON.stringify(expectedList) === JSON.stringify(result)) {
      setCompileError('Correct!!');
    }
    else {
      setCompileError('Wrong Answer!');
    }
  };


  if (level == null) {
    return (<div></div>);
  }

  return (
    <div className='Level'>
      <h3 className='Level__Title'>{ level.title }</h3>
      <p className='Level__Description'>{ level.description }</p>
      <p>{ level.description }</p>
      <div>
        <Collection shapes={ level.startCollection } shaded='' />
      </div>
      <div className='Level__InputBit'>
        <p className="preInput">shapes.</p>
        <input type='text' className="Level__InputForm" value={ userInput } onChange={ e => setUserInput(e.target.value) } />
      </div>
      <button className='Level__Button--Submit' type='submit' onClick={ submitAnswer } >Check Answer</button>
      <p>{ compileError }</p>
      <Collection shapes={ level.expectedCollection } shaded='shaded' />
      <Collection shapes={ queryShapes } shaded='' />
    </div>
  );
}
export default Level;;