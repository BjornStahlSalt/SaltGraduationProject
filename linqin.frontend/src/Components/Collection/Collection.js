import React from 'react';
import Level from './Level';


function Collection({ levels }) {

  return (
    levels.map(level => {
      return <Level key={ level.id } level={ level } />;
    })
  );


}
export default Collection;