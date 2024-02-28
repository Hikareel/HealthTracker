import axios from 'axios';

interface responseModel{
  status: boolean,
  content: any
}

interface formNotificationModel{
  success: string,
  errors: string[]
}

const sendData = async (
  endpoint: string,
  formData: string
  ) => {
    const result: responseModel = {
      status: false,
      content: ""
    }
    try {
      const { data } = await axios.post(
        `/api${endpoint}`,
        formData,
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

export { sendData }
export type { responseModel, formNotificationModel }