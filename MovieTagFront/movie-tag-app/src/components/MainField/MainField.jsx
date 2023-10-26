import './MainField.css';
import { RightField } from '../RightField/RightField';
import { ContentField } from '../ContentField/ContentField';
import TagsContextProvider from '../../Contexts/TagsContext';


const MainField = () => {

    return (
        <div class="mainFieldContainer" >
            <TagsContextProvider>

                <ContentField></ContentField>
                <RightField></RightField>

            </TagsContextProvider>


        </div>


    )
}

export { MainField };