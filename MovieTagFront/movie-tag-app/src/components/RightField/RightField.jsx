import './RightField.css';
import TextField from '@mui/material/TextField';
import { useEffect, useState } from "react";
import {GetTagsByNameAsync} from '../../services/TagService'

const RightField = () =>{
    
    const [searchTag, setSearchTag] = useState("");

    const [tags, setTags ] = useState([]);

    const searchTags = async (e)=>{
        setSearchTag(e.target.value);
        const tagsFromAPI = await GetTagsByNameAsync(e.target.value);
        setTags(tagsFromAPI);
    }

    return(
        <div class ="rightFieldContainer" >

            <div class="usefullTagsContainer" />

            
            <TextField value={searchTag} onChange={searchTags} id="outlined-basic" label="Поиск" variant="outlined" />
            <div class="searchTagsContainer">

                {tags.map(tag=><button>{tag.nameRu}</button>)}

            </div>
           
        
        </div>  
        
          
    )
}

export {RightField} ;