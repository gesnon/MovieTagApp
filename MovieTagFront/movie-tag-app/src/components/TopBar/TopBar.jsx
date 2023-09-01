import './TopBar.css';
import * as React from 'react';
import Button from '@mui/material/Button';
import logo from '../../Image/logo/logo.png';



const TopBar = () =>{

    return(
        <div class ="top-bar-container" >
            <a className ="logo-container" href="#">
                <div class="icon">
                    <img src={logo}/>
                 </div>                
                <div class="logo">Название сайта</div>
            </a> 

            <div class="logInContainer">             
                <Button variant="contained" color="primary">авторизация</Button>   
            </div>
        
        </div>  
        
             
    )
}

export {TopBar} ;