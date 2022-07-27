import React, { useState, useEffect } from 'react';
import Collection from '../Collection/Collection.js';
import Shape from '../Shape/Shape';
import './Result.css';


function Result({ result, shaded, animated }) {
  const [resultHtml, setResultHtml] = useState('');
  useEffect(() => {

    if (Array.isArray(result)) {
      setResultHtml(
        <Collection shapes={ result } shaded={ shaded } animated={ animated } />
      );
      return;
    }

    if (typeof result === 'number') {
      setResultHtml(
        <div>
          <div className='Result__Container'>
            <p className='Result__Text'>{ result }</p>
          </div>
        </div>
      );
      return;
    }

    if (typeof result === 'boolean') {
      setResultHtml(
        <div>
          <div className='Result__Container'>
            <p className='Result__Text'>{ result.toString() }</p>
          </div>
        </div>
      );
      return;
    }

    if (typeof result === 'object') {
      console.log('We got an object');
      setResultHtml(
        <div>
          <div className='Result__Container'>
            <Shape shape={ result } shaded={ shaded } animated={ animated } large='' />
          </div>
        </div>
      );
      return;
    }


  }, [result]);

  return (
    <div className='Result'>
      { resultHtml }
    </div>
  );
}
export default Result;