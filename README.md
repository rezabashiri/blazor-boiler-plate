<div id="top"></div>

<div align="center">
  <a href="https://github.com/rezabashiri/blazor-boiler-plate">
   </a>

  <h3 align="center">Dotnet6 blazor clean code, CQRS ready, hangfire and OpenId connect configured template, blazor login with jwt</h3>

  <p align="center">
    An easy to use template to show how to develop CQRS and background jobs by .net 6 in an micro-service architecture
    <a href="https://github.com/rezabashiri/blazor-boiler-plate/issues">Report Bug</a>
    ·
    <a href="https://github.com/rezabashiri/blazor-boiler-plate/issues">Request Feature</a>
  </p>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->

## About The Project

You can see plenty of individual projects on .net clean code or CQRS implementation in .net, but what if you need all those in micro-service enabled template? Or

What if you need to authorize clients by passwordflow?
What if you are new in blazor type and need to authenticate clients by JWT in it?
Are you exhausted of initiating project and do repeatedly efforts project to project?
Would you like to a have a separate servie to not only authorize your api by JWT but also authorize your Blazor clients interatedly?

What you will achieve :

- A micro-service architectured OpenIdConnect and identity authorization service
- A clean code and Multi-Tiered structure
- Hangfire installed and configured to do back ground jobs
- CQRS pattern enabled by using of MediatR library
- Unit and integration tests
- Configuration and usage of AutoMapper library
- Good example of how to use docker compose to connect two project by docker compose

<p align="right">(<a href="#top">back to top</a>)</p>

### Built With

- [.net6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [MediatR](https://github.com/jbogard/MediatR)
- [AutoMapper](https://github.com/AutoMapper/AutoMapper)
- [Openiddict-Core](https://github.com/openiddict/openiddict-core)
- [EfCore](https://github.com/dotnet/efcore)

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- GETTING STARTED -->

## Getting Started

This is an example of how you may give instructions on setting up your project locally.
To get a local copy up and running follow these simple example steps.

### Prerequisites

This is an example of how to list things you need to use the software and how to install them.
Before all, make sure you have installed .net6 in your machine!

- clone the repo
  ```sh
  git clone https://github.com/rezabashiri/blazor-boiler-plate.git
  ```
- dotnet
  ```sh
  dotnet build ./"Blazor boilerplate.sln"
  ```

### Installation

_Below is how you can run your project via docker compose._

1. Clone the repo
   ```sh
   git clone https://github.com/rezabashiri/blazor-boiler-plate.git
   ```
2. Run docker
   ```sh
   docker compose up -d
   ```
3. [Head to https://localhost:9001](https://localhost:9001)

4. Use Email: rzbashiri@gmail.com and Password: Reza123$%^ to login

5. [Refer to http://localhost:9002/swagger/](http://localhost:9002/swagger/) to see available APIs

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- ROADMAP -->

## Roadmap

- [x] Add CQRS pattern
- [x] Add hangfire
- [x] Add authentication and authorization services
- [ ] Write instrcution document
- [ ] Add ability of CRUD on users

See the [open issues](https://github.com/rezabashiri/blazor-boiler-plate/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- CONTRIBUTING -->

## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- LICENSE -->

## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- CONTACT -->

## Contact

Reza Bashiri - [https://rezabashiri.com/](https://rezabashiri.com/) - rzbashiri@gmail.com - [Linkedin](https://www.linkedin.com/in/reza-bashiri/)

Project Link: [https://github.com/rezabashiri/blazor-boiler-plate](https://github.com/rezabashiri/blazor-boiler-plate)

<p align="right">(<a href="#top">back to top</a>)</p>
