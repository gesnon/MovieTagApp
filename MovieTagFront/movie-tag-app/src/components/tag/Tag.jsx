import './Tag.css';

const Tag = (props) => {

    return (

        <div>
            <div id="tag_cont">                
                                      
                <div class="tagDeleteButton" onClick={p => props.removeUsefullTag(props.nameRu)}> </div>
                
                <span class='tagName'>{props.nameRu}</span>

            </div>


        </div>




    )

}

export { Tag }