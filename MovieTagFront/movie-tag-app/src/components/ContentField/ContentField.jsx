import './ContentField.css';
import { MovieCard } from '../MovieCard/MovieCard';
import { useContext, useEffect, useState } from "react";
import { GetMoviesByTagsAsync } from '../../services/MovieService'
import { TagsContext } from '../../Contexts/TagsContext';

const ContentField = () => {

    const [movies, setMovies] = useState([]);

    const {usefullTags} = useContext(TagsContext)

    const getMovies = async () => {

        const moviesFromAPI = await GetMoviesByTagsAsync(usefullTags);

        setMovies(moviesFromAPI);
    }
    useEffect(() => { getMovies() }, [usefullTags])

    return (
        <div class="contentFieldContainer" >

            {
                movies.map(movie =>
                    <MovieCard nameRu={movie.nameRu}></MovieCard>

                )
            }

        </div>

    )
}

export { ContentField };