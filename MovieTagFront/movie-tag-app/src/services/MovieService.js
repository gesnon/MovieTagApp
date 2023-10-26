import axios from 'axios';


export async function GetMoviesByTagsAsync(tags) {
    const response = await axios.get(`http://37.140.241.219/api/Movie?tags=${tags}`);

    return response.data;
}