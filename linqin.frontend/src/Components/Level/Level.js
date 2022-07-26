import React, { useState, useEffect } from 'react';
import Collection from '../Collection/Collection.js';
import SubmitButton from './SubmitButton';
import './Level.css';
import PropertyList from '../ProperyList/PropertyList.js';
import Result from '../Result/Result.js';

function Level({ level }) {
  const [userInput, setUserInput] = useState("");
  const [compileError, setCompileError] = useState("");
  const [queryResult, setQueryResult] = useState([]);
  const [expectedResult, setExpectedResult] = useState([]);
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
          console.log(response.listOfShapes);
          checkAnswer(level.expectedCollection, response.listOfShapes);
          setTimeout(() => {
            setLoading(false);
          }, 1000);
        }
        else {
          setQueryResult([]);
          setCompileError(response.errorMessage);
          setTimeout(() => {
            setLoading(false);
          }, 1000);

        }
      })
      .catch(error => console.log(error));
  };

  const checkAnswer = (expected, result) => {
    if (Array.isArray(result)) {
      console.log('We got an array');

      const expectedList = expected.map(s => ({ shape: s.shape, color: s.color, priorityValue: s.priorityValue }));
      const resultList = result.map(s => ({ shape: s.shape, color: s.color, priorityValue: s.priorityValue }));

      if (JSON.stringify(expectedList) === JSON.stringify(resultList)) {
        setCompileError('Correct!!');
      }
      else {
        setCompileError('Wrong Answer!');
      }
      setQueryResult(resultList);

      return;
    }

    if (typeof result === 'number') {
      console.log('We got a number');
      setQueryResult(result);
      return;
    }

    if (typeof result === 'boolean') {
      console.log('We got a bool');
      setQueryResult(result);
      return;
    }

    if (typeof result === 'object') {
      console.log('We got an object');
      setQueryResult(result);
      return;
    }


    console.log('Could not read response');
  };

  const updateInput = (e) => {
    setUserInput(e.target.value);
    setQueryShapes([]);
    setCompileError('');
  };

  useEffect(() => {
    setQueryShapes([]);
    setUserInput('');
    setCompileError('');

    console.log('Here');
    if (level) {
      console.log('There');
      console.log(level);

      let temp = expectedResult;
      if (Array.isArray(level.expectedCollection) && level.expectedCollection) {
        console.log('expected array');
        temp = level.expectedCollection;
      }
      else if (level.expectedInt) {
        temp = level.expectedInt;
      }
      else if (level.expectedBool) {
        temp = level.expectedBool;
      }
      else if (level.expectedSingle) {
        console.log('expected object');
        temp = level.expectedSingle;
      }
      setExpectedResult(temp);
    }


  }, [level]);


  if (level == null) {
    return (<div></div>);
  }

  return (
    <div className='Level'>
      <PropertyList shapes={ level.startCollection } />
      <h3 className='Level__Title'>{ level.title }</h3>
      <p className='Level__Description'>{ level.description }</p>
      <div className='Level__Content'>
        <div>
          <Collection shapes={ level.startCollection } shaded='' />
        </div>
        <form className='Level__InputBit' onSubmit={ e => handleSubmit(e) }>
          <p className="preInput">shapes.</p>
          <input type='text' className="Level__InputForm" value={ userInput } onChange={ e => updateInput(e) } />
        </form>
        <button className='Level__Button--Submit' type='submit' onClick={ submitAnswer } >Check Answer</button>
        <p>{ compileError }</p>
        <SubmitButton submitAnswer={ submitAnswer } loading={ loading } compileError={ compileError } />
        <Result result={ expectedResult } shaded='shaded' />
        <Result result={ queryResult } shaded='' />
      </div>
    </div>
  );
}
export default Level;