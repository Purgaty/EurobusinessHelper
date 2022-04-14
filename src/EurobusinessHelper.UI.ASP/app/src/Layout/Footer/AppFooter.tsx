import axios from "axios";
import React, { useEffect } from 'react'
import config from "../../app/config";
import IdentityService from "../../Services/IdentityService";
import {setIdentity} from "./identityReducer";

export const AppFooter = () => {
  useEffect(() => {
    axios.get(config.apiUrl + "/api/auth/test").then(res => res.data);
    IdentityService.getCurrentIdentity().then(result => setIdentity(result));
  });
  
  return (
    <div className='application-footer'>FOOTER</div>
  )
}
