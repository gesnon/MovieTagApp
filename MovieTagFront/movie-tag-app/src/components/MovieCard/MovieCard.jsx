import './MovieCard.css';

const MovieCard = (props) =>{

    return (
        <div className='movieCardContainer'>
            <span>{props.nameRu} </span>
        </div>
    )

}

export {MovieCard}