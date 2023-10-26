/*jslint es6:true*/
import './RightField.css';
import TextField from '@mui/material/TextField';
import { useContext, useEffect, useState } from "react";
import { GetTagsByNameAsync } from '../../services/TagService'
import { Tag } from '../tag/Tag';
import { TagsContext } from '../../Contexts/TagsContext';


const RightField = () => {

    const [searchTag, setSearchTag] = useState("");

    const [tags, setTags] = useState([]);

    const { usefullTags, setUsefullTags } = useContext(TagsContext)

    const [uselessTags, setUselessTags] = useState([]);

    const searchTags = async (e) => {
        setSearchTag(e.target.value);
        const tagsFromAPI = await GetTagsByNameAsync(e.target.value);
        setTags(tagsFromAPI);
    }

    const removeUsefullTag = (nameRu) => {

        setUsefullTags(t => t.filter(name => name != nameRu));
    }

    return (
        <div class="rightFieldContainer" >
            <div className='acceptedTagsContainer'>

                <div class="usefullTagsContainer">

                    {
                        usefullTags.map(tag =>
                            <Tag nameRu={tag} removeUsefullTag={removeUsefullTag} ></Tag>

                        )
                    }

                </div>

                <div class="uselessTagsContainer">

                </div>
            </div>

            <div class="searchFieldContainer">
                <TextField value={searchTag} onChange={searchTags} id="outlined-basic" label="Поиск" variant="outlined" />
            </div>
            
            <div class="searchTagsContainer">

                <div className='tag-Container'>

                    {
                        tags.map(tag =>
                            <div id="btn_cont">
                                <div class='splitButtonContainer'>
                                    <div class="btn" onClick={e => setUsefullTags(t => [...t, tag.nameRu])}> </div>
                                    <div class="btn"> </div>
                                </div>
                                <span class='tagName'>{tag.nameRu}</span>

                            </div>

                        )
                        }

                </div>


            </div>


        </div>


    )
}

export { RightField };