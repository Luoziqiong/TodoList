<template>
    <div id="register">
      <h1 class="title">用户注册</h1>
        <el-form :label-position="labelPosition" label-width="80px" ref="registerForm" :model="registerForm">
            <el-form-item label="用户名">
                <el-input v-model="registerForm.name"></el-input>
            </el-form-item>
            <el-form-item label="密码">
                <el-input type="password" v-model="registerForm.passowrd"></el-input>
            </el-form-item>
            
            <el-button type="primary" @click="register(registerForm)">注册</el-button>
        </el-form>
        <div class="to-login">
           <p>已经有账号？ <el-button class="login" type="text" @click="toLogin">直接登录>></el-button></p>
           
        </div>
    </div>
</template>
<script>
var qs = require("qs");
export default {
  data() {
    return {
      labelPosition: "left",
      registerForm: {
        name: "",
        password: ""
      }
    };
  },
  methods: {
    login(formName) {
      var user = this.loginForm;
      this.axios
        .post(
          "/api/user",
         {
            count: this.loginForm.count,
            password: this.loginForm.password
          }
        )
        .then(r => {
          console.log(r);
          if (r.data === 1) {
            this.$message.suceess("注册成功处理");
          } else if (res.data === -1) {
             this.$message.error("该账户已存在"); //失败处理
          } else {
             this.$message.error("注册失败处理"); //失败处理
          }
        })
        .catch(err => {});
    },
    toLogin() {
      this.$router.push({ path: "/login" });
    }
  }
};
</script>
<style scoped>
#register {
  width: 400px;
  height: 300px;
  margin: 100px auto;
  padding: 50px 40px;
  border-radius: 10px;
  background: #fff;
  box-shadow: 1px 1px 10px 0 #979797;
}
.title{
  margin-top: 0;
  color:#398bdd;
  font-weight: 400;
}
p{
  font-size:14px;
}
</style>
