import React, { useState, useEffect } from 'react';
import Collection from '../Collection/Collection.js';
import Shape from '../Shape/Shape';
import './Result.css';


function Result({ result, shaded }) {
  const [resultHtml, setResultHtml] = useState('');
  useEffect(() => {

    if (Array.isArray(result)) {
      setResultHtml(
        <Collection shapes={result} shaded='shaded' />
      );
      return;
    }

    if (typeof result === 'number') {
      setResultHtml(<p className='Result__Text'>{result}</p>);
      return;
    }

    if (typeof result === 'boolean') {
      setResultHtml(<p className='Result__Text'>{result.toString()}</p>);
      return;
    }

    if (typeof result === 'object') {
      console.log('We got an object');
      setResultHtml(
        <Shape shape={result} shaded={shaded} large='' />
      );
      return;
    }


  }, [result]);

  return (
    <div className='Result'>
      {resultHtml}
    </div>
  );
}
export default Result;