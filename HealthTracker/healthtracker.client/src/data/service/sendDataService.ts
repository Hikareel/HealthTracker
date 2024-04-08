import axios from 'axios';
import router from '@/router'
import { ref } from 'vue';

interface IResponseModel{
  status: boolean,
  content: any
}

interface IFormStatusModel{
  success: string,
  errors: string[]
}

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
  responseContent: any
) => {
  if(endpoint == "/login"){
    localStorage.setItem("token", responseContent.token);
    localStorage.setItem("userId", responseContent.userId);
    formStatus.value.success = "User is logged"
    router.push('/').then(() =>{
        window.location.reload()
    });
  } else{
    formStatus.value.success = responseContent
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
      result.content = { token: data.token, userId: data.userId };
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
    const response: IResponseModel = await sendData(endpoint, data)
    if(response.status){
      tasksForEndpoint(endpoint, response.content)
    } else {
      response.content.forEach((element: { description: string; }) => {
        formStatus.value.errors.push(element.description)
      });
    }
  }

export { preventSubmit, formStatus, clearFormStatus }