import axios from 'axios';


export async function GetTagsByNameAsync(name) {
    const response = await axios.get(`http://37.140.241.219/api/Tag?name=${name}`);

    return response.data;
}


