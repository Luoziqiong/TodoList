<template>

    <div id="login">
      <h1 class="title">用户登录</h1>
        <el-form :label-position="labelPosition" label-width="80px" ref="loginForm" :model="loginForm">
            <el-form-item label="用户名">
                <el-input v-model="loginForm.name"></el-input>
            </el-form-item>
            <el-form-item label="密码">
                <el-input type="password" v-model="loginForm.passowrd"></el-input>
            </el-form-item>
           
                <el-button type="primary" @click="login(loginForm)">登录</el-button>
           
        </el-form>
        <div class="to-register">
           <p>还没有账号？
            <el-button class="register" type="text" @click="toRegister">免费注册>></el-button>
           </p>
        </div>
    </div>
</template>
<script>
var qs = require("qs");
export default {
  data() {
    return {
      labelPosition: "left",
      loginForm: {
        name: "",
        password: ""
      }
    };
  },
  methods: {
    login(formName) {
      var user = this.loginForm;
      console.log(user);
      this.axios
        .get("/api/User", {
          count: this.loginForm.count,
          password: this.loginForm.password
        })
        .then(r => {
          console.log(r);
          if (r.data) {
            this.$router.push({
              path: "./todoList",
              query: { userId: r.data }
            });
          } else {
            alert("登录失败处理"); //失败处理
          }
        })
        .catch(err => {});
    },
    toRegister() {
      this.$router.push({ path: "/register" });
    }
  }
};
</script>
<style scoped>
#login {
  width: 400px;
  height: 300px;
  margin: 100px auto;
  padding: 50px 40px;
  border-radius: 10px;
  background: #fff;
  box-shadow: 1px 1px 10px 0 #979797;
}

.title {
  margin-top: 0;
  color: #398bdd;
  font-weight: 400;
}
p{
  font-size:14px;
}
</style>