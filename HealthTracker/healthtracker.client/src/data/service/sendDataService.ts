import axios from 'axios';
import router from '@/router'
import { ref } from 'vue';

interface responseModel{
  status: boolean,
  content: any
}

interface formNotificationModel{
  success: string,
  errors: string[]
}

const formNotification = ref<formNotificationModel>({
  success: "",
  errors: []
})

const clearNotification = () => {
  formNotification.value.errors = []
  formNotification.value.success = ""
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
    clearNotification()
    let response: responseModel = await sendData(endpoint, data)
    if(response.status){
      if(endpoint == "/login"){
        localStorage.setItem("token", response.content)
        formNotification.value.success = "User is logged"
        router.push('/').then(() =>{
            window.location.reload()
        });
      } else{
        formNotification.value.success = response.content
        document.getElementById('reset_button')!.click()
      }
    } else {
      response.content.forEach((element: { description: string; }) => {
        formNotification.value.errors.push(element.description)
      });
    }
  }

export { preventSubmit, formNotification, clearNotification }