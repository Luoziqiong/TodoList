<template>
    <div id="register">
      <h1 class="title">用户注册</h1>
        <el-form 
        :rules="registerRules" 
        label-width="0px" 
        ref="registerForm" 
        :model="registerForm"
        class="demo-ruleForm">
            <el-form-item  prop="name">
                <el-input v-model="registerForm.name" placeholder="用户名"></el-input>
            </el-form-item>
            <el-form-item  prop="password">
                <el-input type="password" v-model="registerForm.password" placeholder="密码"></el-input>
            </el-form-item>
            
            <el-button type="primary" @click="register('registerForm')" class="registerBtn">注册</el-button>
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
      },
      registerRules: {
        name: [
          { required: true, message: "请输入用户名", trigger: "blur" },
          { min: 3, max: 10, message: "长度在 3 到 10 个字符", trigger: "blur" }
        ],
        password: [
          { required: true, message: "请输入密码", trigger: "blur" },
          { min: 3, max: 10, message: "长度在 3 到 10 个字符", trigger: "blur" }
        ]
      }
    };
  },
  methods: {
    register(formName) {
      this.$refs[formName].validate(valid => {
        if (valid) {
          this.axios
            .post("/api/User/Register", {
              Username: this.registerForm.name,
              Password: this.registerForm.password
            })
            .then(r => {
              console.log(r);
              if (r.data == 1) {
                this.$message({
                  message: "注册成功!",
                  type: "suceess"
                });
              } else if (r.data == -1) {
                this.$message({
                  message: "该账户已存在!",
                  type: "error"
                }); //失败处理
              } else {
                this.$message({
                  message: "注册失败处理!",
                  type: "error"
                }); //失败处理
              }
            })
            .catch(err => {});
        } else {
          console.log("");
          return false;
        }
      });
    },
    toLogin() {
      this.$router.push({ path: "/login" });
    }
  }
};
</script>
<style scoped>
#register {
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
.registerBtn {
  width: 100%;
}
</style>
