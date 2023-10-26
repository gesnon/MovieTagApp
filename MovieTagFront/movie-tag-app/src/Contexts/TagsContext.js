import { createContext, useState } from "react";

export const TagsContext = createContext(null);

const TagsContextProvider = (props) => {

    const [usefullTags, setUsefullTags] = useState([]);

    return <TagsContext.Provider value={{ usefullTags, setUsefullTags }}>{props.children}</TagsContext.Provider>
}
export default TagsContextProvider;