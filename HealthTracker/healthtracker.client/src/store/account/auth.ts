import { defineStore } from 'pinia';

interface IUser {
    userId: number | null;
    userName: string | null;
    firstName: string | null;
    lastName: string | null;
    email: string | null;
    loginTime: string | null;
    token: string | null;
}

export const useUserStore = defineStore('userData', {
    state: (): IUser => ({
        userId: null,
        userName: null,
        firstName: null,
        lastName: null,
        email: null,
        loginTime: null,
        token: null,
    }),
    actions: {
        setUserData(userData: IUser) {
            this.userId = userData.userId;
            this.userName = userData.userName;
            this.firstName = userData.firstName;
            this.lastName = userData.lastName;
            this.email = userData.email;
            this.loginTime = userData.loginTime;
            this.token = userData.token;
        },
        clearUserData() {
            this.userId = null;
            this.userName = null;
            this.firstName = null;
            this.lastName = null;
            this.email = null;
            this.loginTime = null;
            this.token = null;
        },
        updateUserData(){
            console.log("Updating user in store...")
            const userString = localStorage.getItem("user");
            if (userString) {
                const data = JSON.parse(userString);
                this.userId = data.userId;
                this.userName = data.userName;
                this.firstName = data.firstName;
                this.lastName = data.lastName;
                this.email = data.email;
                this.loginTime = data.loginTime;
                this.token = data.token;
              } 
              else {
                this.userId = null;
                this.userName = null;
                this.firstName = null;
                this.lastName = null;
                this.email = null;
                this.loginTime = null;
                this.token = null;
              }
        }
    }
});
