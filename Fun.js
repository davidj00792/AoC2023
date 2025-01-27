function moveZeros(arr) {
    if (arr.length === 0) {
        console.log('Hi');
        return;}
    
    const removeZeros = arr.filter(element => element !== 0);
  
    const numberOfZeros = arr.length - removeZeros.length;
  
    const zeros = new Array(numberOfZeros).fill(0);
    console.log('yo');
    return removeZeros.concat(zeros);
  }


  const array = [];
  moveZeros(array);

