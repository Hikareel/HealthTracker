import axios from 'axios';
import router from '@/router'
import { ref } from 'vue';
import type { IFormStatusModel, IResponseModel} from '../models/formDataModels'

const formStatus = ref<IFormStatusModel>({
  success: "",
  errors: []
})

const clearFormStatus = () => {
  formStatus.value.errors = []
  formStatus.value.success = ""
}

const tasksForEndpoint = (
  endpoint: string,
  responseCentent: any
) => {
  if(endpoint == "/login"){
    localStorage.setItem("token", responseCentent)
    formStatus.value.success = "User is logged"
    router.push('/').then(() =>{
        window.location.reload()
    });
  } else{
    formStatus.value.success = responseCentent
    document.getElementById('reset_button')!.click()
  }
}

const sendData = async (
    endpoint: string,
    postData: string
  ) => {
    const result: IResponseModel = {
      status: false,
      content: ""
    }
    try {
      const { data } = await axios.post(
        `/api${endpoint}`,
        postData,
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );
      result.status = true
      if(endpoint == "/login")
        result.content = data.token
      else 
        result.content = data.message
    } catch (error: any) {
      result.status = false
      result.content = error.response.data
    }
    return result
  }

const preventSubmit = async (
    endpoint: string,
    data: string
  ) => {
    clearFormStatus()
    let response: IResponseModel = await sendData(endpoint, data)
    if(response.status){
      tasksForEndpoint(endpoint, response.content)
    } else {
      response.content.forEach((element: { description: string; }) => {
        formStatus.value.errors.push(element.description)
      });
    }
  }

export { preventSubmit, formStatus, clearFormStatus }