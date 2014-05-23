pokitdok-php
=============

PokitDok [Platform API][apidocs] Client for PHP

## Installation
Simply add a dependency on pokitdok/pokitdok-php to your project's composer.json file if you use Composer to manage the dependencies of your project. Here is a minimal example of a composer.json:

    {
        "require": {
            "pokitdok/pokitdok-php": "*"
        }
    }

## Tests
    phpunit src/PokitDok/Tests/

## Resources
* [Read the PokitDok API docs][apidocs]
* [View Source on GitHub][code]
* [Report Issues on GitHub][issues]

[apidocs]: https://platform.pokitdok.com/dashboard#/documentation
[code]: https://github.com/PokitDokInc/pokitdok-php
[issues]: https://github.com/PokitDokInc/pokitdok-php/issues

## Usage Example

    $ php -a

    php > include 'vendor/autoload.php';
    php > $client = new PokitDok\Platform\PlatformClient('your_client_id', 'your_client_secret');
    php > $eligibility_request = array('payer_id' => "MOCKPAYER", 'member_id' => "W34237875729", 'provider_id' => "1467560003", 'provider_name' => "AYA-AY", 'provider_first_name' => "JEROME", 'provider_type' => "Person", 'member_name' => "JOHN DOE", 'member_birth_date' => "05/21/1975", 'service_types' => array("Health Benefit Plan Coverage"));
    php > $eligibility_response = $client->eligibility($eligibility_request)->body();

    php > var_dump($eligibility_response);

    class stdClass#5 (2) {
      public $meta =>
      class stdClass#79 (7) {
        public $rate_limit_amount =>
        int(3)
        public $rate_limit_reset =>
        int(1398806150)
        ...
      }
      public $data =>
      class stdClass#80 (24) {
        public $provider_id =>
        string(10) "1467560003"
        public $client_id =>
        ...

## Supported PHP Versions
This library aims to support and is tested against these PHP versions:

* php >= 5.3

You may have luck with other interpreters - let us know how it goes.

## License
Copyright (c) 2014 PokitDok Inc. See [LICENSE][] for details.

[license]: LICENSE.txt


