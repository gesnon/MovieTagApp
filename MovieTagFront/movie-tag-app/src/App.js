import "./App.css";
import { MainField } from "./components/MainField/MainField";
import {TopBar} from './components/TopBar/TopBar.jsx';

function App() {
    return (
        <div className="appContainer">

            <TopBar></TopBar>    

            <div >         

                <MainField></MainField>

            </div>         
                        
        </div>
    );
}

export default App;
