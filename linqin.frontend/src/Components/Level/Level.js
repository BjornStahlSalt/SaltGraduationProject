import React, { useState, useEffect } from 'react';
import Collection from '../Collection/Collection.js';
import SubmitButton from './SubmitButton';
import ClipLoader from "react-spinners/ClipLoader";
import './Level.css';


function Level({ level }) {
  const [userInput, setUserInput] = useState("");
  const [compileError, setCompileError] = useState(null);
  const [queryShapes, setQueryShapes] = useState([]);
  const [loading, setLoading] = useState(false);

  const handleSubmit = (e) => {
    e.preventDefault();
    submitAnswer();
  };

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
      .then(setLoading(true))
      .then(response => {
        if (response.errorMessage === null) {
          setQueryShapes(response.listOfShapes);
          checkAnswer(level.expectedCollection, response.listOfShapes);
          setLoading(false);
        }
        else {
          setQueryShapes([]);
          setCompileError(response.errorMessage);
          setLoading(false);

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

  const updateInput = (e) => {
    setUserInput(e.target.value);
    setQueryShapes([]);
  };

  useEffect(() => {
    setQueryShapes([]);
    setUserInput('');

  }, [level]);


  if (level == null) {
    return (<div></div>);
  }

  return (
    <div className='Level'>
      <h3 className='Level__Title'>{ level.title }</h3>
      <p className='Level__Description'>{ level.description }</p>
      <div>
        <Collection shapes={ level.startCollection } shaded='' />
      </div>
      <form className='Level__InputBit' onSubmit={ e => handleSubmit(e) }>
        <p className="preInput">shapes.</p>
        <input type='text' className="Level__InputForm" value={ userInput } onChange={ e => updateInput(e) } />
      </form>
      {/* <button className='Level__Button--Submit' type='submit' onClick={ submitAnswer } >Check Answer</button> */ }
      <p>{ loading ? <ClipLoader color={ '#36D7B7' } loading={ loading } size={ 30 } /> : compileError }</p>
      <SubmitButton submitAnswer={ submitAnswer } loading={ loading } compileError={ compileError } />
      <Collection shapes={ level.expectedCollection } shaded='shaded' />
      <Collection shapes={ queryShapes } shaded='' />
    </div>
  );
}
export default Level;;