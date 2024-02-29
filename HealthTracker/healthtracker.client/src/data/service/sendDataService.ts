import axios from 'axios';
import router from '@/router'
import { ref } from 'vue';

interface responseModel{
  status: boolean,
  content: any
}

interface formStatusModel{
  success: string,
  errors: string[]
}

const formStatus = ref<formStatusModel>({
  success: "",
  errors: []
})

const clearFormStatus = () => {
  formStatus.value.errors = []
  formStatus.value.success = ""
}

const sendData = async (
    endpoint: string,
    postData: string
  ) => {
    const result: responseModel = {
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
    let response: responseModel = await sendData(endpoint, data)
    if(response.status){
      if(endpoint == "/login"){
        localStorage.setItem("token", response.content)
        formStatus.value.success = "User is logged"
        router.push('/').then(() =>{
            window.location.reload()
        });
      } else{
        formStatus.value.success = response.content
        document.getElementById('reset_button')!.click()
      }
    } else {
      response.content.forEach((element: { description: string; }) => {
        formStatus.value.errors.push(element.description)
      });
    }
  }

export { preventSubmit, formStatus, clearFormStatus }