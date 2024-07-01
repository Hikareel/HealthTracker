//To gówno jest całe do zmiany. Fakty są takie że trzeba zrobić jakiś system do strzelania w endpointy ale na to się patzrzeć nie da.
import axios from "axios";
import router from "@/router";
import { ref } from "vue";
import { useUserStore } from '../../../store/account/auth';

interface IResponseModel {
  status: boolean,
  content: any
}

interface IFormStatusModel {
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
  if (endpoint == "/login") {
    localStorage.setItem("user", JSON.stringify(responseContent))
    formStatus.value.success = "User is logged"
    const userStore = useUserStore();
    router.push("/").then(() => {
      userStore.updateUserData();
    });
  } else {
    formStatus.value.success = responseContent
    document.getElementById("reset_button")!.click()
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
    });
    result.status = true;
    if (endpoint == "/login") {
      result.content = data;
    } else {
      result.content = data.message;
    }
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
    if (response.status) {
        tasksForEndpoint(endpoint, response.content)
    } else {
        response.content.forEach((element: { description: string }) => {
            formStatus.value.errors.push(element.description)
        });
    }
}

export { preventSubmit, formStatus, clearFormStatus }