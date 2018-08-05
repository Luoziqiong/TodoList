<template>

    <div id="login">
      <h1 class="title">用户登录</h1>
        <el-form
        label-width="0px" 
        ref="loginForm" 
        :model="loginForm"
        :rules="loginRules"
        class="demo-ruleForm">
            <el-form-item prop="name">
                <el-input v-model="loginForm.name" placeholder="用户名"></el-input>
            </el-form-item>
            <el-form-item prop="password">
                <el-input type="password" v-model="loginForm.password" placeholder="密码"></el-input>
            </el-form-item>
            <el-button type="primary" @click="login(loginForm)" class="loginbtn">登录</el-button>
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
      },
      loginRules: {
        name: [
          { required: true, message: "请输入用户名", trigger: "blur" },
          { min: 3, max: 5, message: "长度在 3 到 5 个字符", trigger: "blur" }
        ],
        password: [
          { required: true, message: "请输入密码", trigger: "blur" },
          { min: 3, max: 5, message: "长度在 3 到 5 个字符", trigger: "blur" }
        ]
      }
    };
  },
  methods: {
    login(formName) {
      var user = this.loginForm;
      console.log(user);
      // this.axios.get("/api/User", {
      //     count: this.loginForm.count,
      //     password: this.loginForm.password
      //   })
      //   .then(r => {
      //     console.log(r);
      //     if (r.data) {
      //       this.$router.push({
      //         path: "./todoList",
      //         query: { userId: r.data }
      //       });
      //     } else {
      //       alert("登录失败处理"); //失败处理
      //     }
      //   })
      //   .catch(err => {});
      this.$router.push({ path: "/todoList" });
    },
    toRegister() {
      this.$router.push({ path: "/register" });
    }
  }
};
</script>
<style scoped>
#login {
  width: 300px;
  height: 260px;
  margin: 80px auto;
  padding: 50px;
  border-radius: 10px;
  background: #fff;
  box-shadow: 1px 1px 10px 0 #979797;
}
.title {
  margin-top: 0;
  color: #398bdd;
  font-weight: 400;
}
p {
  font-size: 14px;
}
.loginbtn {
  width: 100%;
}
</style>