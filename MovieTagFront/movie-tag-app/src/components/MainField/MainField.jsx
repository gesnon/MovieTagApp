import './MainField.css';
import { RightField } from '../RightField/RightField';
import { ContentField } from '../ContentField/ContentField';

const MainField = () =>{

    return(
        <div class ="mainFieldContainer" >
            
            <ContentField></ContentField>
            <RightField></RightField>
        
        </div>  
        
             
    )
}

export {MainField} ;